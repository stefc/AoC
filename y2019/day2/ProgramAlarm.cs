// http://adventofcode.com/2019/day/2

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace advent.of.code.y2019.day2
{
	using Adr = Int64; 
	using Mem = Int64; 

    using static F;

    using Computation = StatefulComputation<Option<ProgramState>, 
		ImmutableSortedDictionary<Int64,Int64>>;

    public enum Mode
    {
        Position = 0,

        Immediate = 1,
        Relative = 2,
    }

    public enum OpCode
    {
        Nop = 0, 
        Add = 1,
        Mul = 2,

        Input = 3,
        Output = 4,

        JmpIfTrue = 5,

        JmpIfFalse = 6,

        LessThan = 7,

        Equals = 8,

		Adjust = 9,

        Exit = 99,
    }

    public readonly struct ProgramState
    {
        public readonly Adr IP;
		public readonly Adr RelativeBase;
        public readonly ImmutableSortedDictionary<Adr, Mem> Program;

        public readonly ImmutableQueue<Mem> Input;

        public readonly ImmutableStack<Mem> Output;

        public readonly bool Stopped;

        public ProgramState(Adr pc, IEnumerable<Mem> program) :
        this(pc,0,
            program.Aggregate(
            ImmutableSortedDictionary<Adr, Mem>.Empty, 
				(accu, current) => accu.Add(accu.Count(), current)),
            ImmutableQueue<Mem>.Empty, ImmutableStack<Mem>.Empty, false)
        { }

        private ProgramState(Adr ip, Adr relativeBase, 
			ImmutableSortedDictionary<Adr, Mem> program,
            ImmutableQueue<Mem> input, ImmutableStack<Mem> output, 
			bool stopped)
        {
            this.IP = ip;
			this.RelativeBase = relativeBase;
            this.Program = program;
            this.Input = input;
            this.Output = output;
            this.Stopped = stopped;
        }

        public OpCode OpCode => ProgramAlarm.GetOpCode(this);

        public ProgramState WithIncrementIP(Adr step = 4)
            => new ProgramState(this.IP + step, this.RelativeBase, 
				this.Program, this.Input, this.Output, this.Stopped);
		public ProgramState WithAdjust(Adr value)
            => new ProgramState(this.IP, this.RelativeBase+value, 
				this.Program, this.Input, this.Output, this.Stopped);

        public ProgramState WithStopping()
            => new ProgramState(this.IP, this.RelativeBase, this.Program, 
			this.Input, this.Output, true);
        public ProgramState WithRunning()
            => new ProgramState(this.IP, this.RelativeBase, this.Program, 
			this.Input, this.Output, false);

        public ProgramState WithInput(ImmutableQueue<Mem> input)
            => new ProgramState(this.IP, this.RelativeBase, 
				this.Program, input, this.Output, false);
        public ProgramState WithInput(Mem input, params Mem[] more)
            => new ProgramState(this.IP, this.RelativeBase, this.Program,
                more.Aggregate(this.Input.Enqueue(input),
                    (accu, current) => accu.Enqueue(current)),
                 this.Output, false);


        public ProgramState WithIP(Adr ip)
            => new ProgramState(ip, this.RelativeBase, this.Program, this.Input,
                this.Output, this.Stopped);


        public ProgramState WithProgram(ImmutableSortedDictionary<Adr, Mem> program)
            => new ProgramState(this.IP, this.RelativeBase, 
				program, this.Input, this.Output, this.Stopped);
        public ProgramState WithOutput(Mem output)
            => new ProgramState(this.IP, this.RelativeBase, 
				this.Program, this.Input, this.Output.Push(output), 
				this.Stopped);
        public ProgramState WithOutput(ImmutableStack<Mem> output)
            => new ProgramState(this.IP, this.RelativeBase, 
				this.Program, this.Input, output, this.Stopped);

        public ProgramState WithExecute()
        {
            
            return ProgramAlarm.Dispatch(this);
        }

        public Mem Read(Adr adr) 
        => this.Program.TryGetValue(adr, out var value) ? value : 0L;

        public ProgramState Write(Adr adr, Mem value) 
        => this.WithProgram(this.Program.SetItem(adr, value));
    }

    public static class ProgramAlarm
    {

        public static ProgramState CreateProgram(IEnumerable<Mem> program,
            Mem input)
            => new ProgramState(0, program).WithInput(input);
        public static ProgramState CreateProgram(IEnumerable<Mem> program)
            => new ProgramState(0, program);

        public static ProgramState CreateProgram(params Mem[] program)
            => new ProgramState(0, program);


        public static Computation CreateStateMaschine()
        => state =>
                state.Match(
                    None: () => (ImmutableSortedDictionary<Adr,Mem>.Empty, None),
                    Some: s => Exec(s)
                );

        public static (ImmutableSortedDictionary<Adr,Mem> Value, Option<ProgramState> State) Exec(
            ProgramState state)
        {
            while (!state.Stopped) {
                state = state.WithExecute();
            }
            return (Value: state.Program, State: Some(state));
        }

        private static Func<Mem, Mem, Mem> Add() => (a, b) => a + b;
        private static Func<Mem, Mem, Mem> Mul() => (a, b) => a * b;
        private static Func<Mem, Mem, Mem> Eq() => (a, b) => a == b ? 1 : 0;
        private static Func<Mem, Mem, Mem> Lt() => (a, b) => a < b ? 1 : 0;
        private static Func<Mem, bool> True() => x => x != 0;
        private static Func<Mem, bool> False() => x => x == 0;

        private static ProgramState Exec(ProgramState state,
            Func<Mem, Mem, Mem> operation)
        => (
            from a in Get()(state, 1)
            from b in Get()(state, 2)
            from newState in Put()(state, 3, operation(a, b))
            select newState.WithIncrementIP()
        ).GetOrElse(state);

        private static ProgramState ExecInput(ProgramState state)
        => (
            state.Input.IsEmpty ?
            Some(state.WithStopping())
            :
            from newState in Put()
                (state.WithInput(state.Input.Dequeue(out var input)), 1, input)
            select newState.WithIncrementIP(2)
        ).GetOrElse(state);

        private static ProgramState ExecOutput(ProgramState state)
        => (
            from a in Get()(state, 1)
            select state.WithOutput(a).WithIncrementIP(2)
        ).GetOrElse(state);

        private static ProgramState ExecAdjust(ProgramState state) 
        =>  (
            from a in Get()(state, 1)
            select state.WithAdjust(a).WithIncrementIP(2)
        ).GetOrElse(state);


        private static ProgramState ExecJump(ProgramState state,
            Func<Mem, bool> condition)
        => (
            from a in Get()(state, 1)
            from b in Get()(state, 2)
            select condition(a) ? state.WithIP(b) : state.WithIncrementIP(3)
        ).GetOrElse(state);

        private static ProgramState ExecExit(ProgramState state)
        => state.WithStopping();


        public static ProgramState Dispatch(ProgramState state)
        {
            var opCode = ToOpCode(state.Read(state.IP));
            switch (opCode)
            {
                case OpCode.Add:
                    return Exec(state, Add());

                case OpCode.Mul:
                    return Exec(state, Mul());

                case OpCode.Equals:
                    return Exec(state, Eq());

                case OpCode.LessThan:
                    return Exec(state, Lt());

                case OpCode.Input:
                    return ExecInput(state);

                case OpCode.Output:
                    return ExecOutput(state);

                case OpCode.JmpIfTrue:
                    return ExecJump(state, True());

                case OpCode.JmpIfFalse:
                    return ExecJump(state, False());

				case OpCode.Adjust: 
					return ExecAdjust(state);

                case OpCode.Exit:
                    return ExecExit(state);
            }
            return state;
        }

        private static OpCode ToOpCode(Mem value)
        => (OpCode)(value % 100);

		private static Mode ToMode(this Mem flags) 
		=> (Mode)((flags % 10) % 3);

		public static IEnumerable<Mode> ModesFromInstruction(this Mem instruction)
        => 3.PowersOf10()
			.Aggregate(ImmutableList<Mode>.Empty,
			(accu, current) => accu.Add((instruction / (current*100)).ToMode()));

        public static OpCode GetOpCode(ProgramState state)
        => ToOpCode(state.Read(state.IP));

        public static Func<ProgramState, IEnumerable<Mode>> GetModeFlags()
        => state
            => state.Read(state.IP).ModesFromInstruction();

        private static Func<ProgramState, int, Option<Mem>> Get()
        => (state, index) => {
            var modes = GetModeFlags()(state);
            var mode = modes.ElementAtOrDefault(index-1);
            var raw =  state.Read(state.IP + index);
            switch (mode)
            {
                case Mode.Position: 
                    return Some(state.Read(raw));
                case Mode.Immediate:
                    return Some(raw);
                case Mode.Relative:
                    return Some(state.Read(state.RelativeBase+raw));
                default:
                    return Some(0L);
            }
		};

        private static Func<ProgramState, int, Mem, Option<ProgramState>> Put()
        => (state, index, value)
            => {
                var modes = GetModeFlags()(state);
                var raw = state.Read(state.IP + index);
                var mode = modes.ElementAtOrDefault(index-1);
                var isRelative = mode == Mode.Relative;
                return Some(
                    state.Write(raw + (isRelative ? state.RelativeBase:0), 
                    value));
            };
    }
}