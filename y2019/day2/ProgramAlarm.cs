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

        public Option<OpCode> OpCode => ProgramAlarm.GetOpCode()(this);

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
            var getOpCode = ProgramAlarm.GetOpCode();
            var getFunc = ProgramAlarm.Dispatch();
            var state = this;
            return (
                from code in getOpCode(state)
                from newState in getFunc(code)(state)
                select newState).GetOrElse(state);
        }

        public Mem Read(Adr adr) 
        => this.Program.TryGetValue(adr, out var value) ? value : 0L;

        public static Option<ProgramState> Exec(ProgramState state,
            Func<Mem, Mem, Mem> operation)
        {
            var getValue = ProgramAlarm.GetInt();
            var putValue = ProgramAlarm.PutInt();
            var modes = ProgramAlarm.GetModeFlags()(state);

            return
                from a_ in getValue(state, state.IP + 1, Mode.Position)
                from a in getValue(state, a_, modes.ElementAtOrDefault(0))
                from b_ in getValue(state, state.IP + 2, Mode.Position)
                from b in getValue(state, b_, modes.ElementAtOrDefault(1))
                from ptr in getValue(state, state.IP + 3, Mode.Position)
                from newState in putValue(state, ptr, modes.ElementAtOrDefault(2), operation(a, b))
                select newState.WithIncrementIP();
        }

        public static Option<ProgramState> ExecInput(ProgramState state)
        {
            var getValue = ProgramAlarm.GetInt();
            var putValue = ProgramAlarm.PutInt();
			var modes = ProgramAlarm.GetModeFlags()(state);

            if (state.Input.IsEmpty)
            {
                return Some(state.WithStopping());
            }
            else
            {
                var newInput = state.Input.Dequeue(out var input);
                state = state.WithInput(newInput);
                return
                    from ptr in getValue(state, state.IP + 1, Mode.Position)
                    from newState in putValue(state, ptr, 
                            modes.ElementAtOrDefault(0), input)
                    select newState.WithIncrementIP(2);
            }
        }

        public static Option<ProgramState> ExecOutput(ProgramState state)
        {
            var getValue = ProgramAlarm.GetInt();
            var putValue = ProgramAlarm.PutInt();
            var modes = ProgramAlarm.GetModeFlags()(state);
            return
                from a_ in getValue(state, state.IP + 1, Mode.Position)
                from a in getValue(state, a_, modes.ElementAtOrDefault(0)  )
                select state.WithOutput(a).WithIncrementIP(2);
        }
		public static Option<ProgramState> ExecAdjust(ProgramState state)
        {
            var getValue = ProgramAlarm.GetInt();
            var putValue = ProgramAlarm.PutInt();
            var modes = ProgramAlarm.GetModeFlags()(state);
            return
                from a_ in getValue(state, state.IP + 1, Mode.Position)
                from a in getValue(state, a_, modes.ElementAtOrDefault(0)  )
                select state.WithAdjust(a).WithIncrementIP(2);
        }


        public static Option<ProgramState> ExecJump(ProgramState state,
            Func<Mem, bool> condition)
        {
            var getValue = ProgramAlarm.GetInt();
            var putValue = ProgramAlarm.PutInt();
            var modes = ProgramAlarm.GetModeFlags()(state);
            return
                from a_ in getValue(state, state.IP + 1, Mode.Position)
                from a in getValue(state, a_, modes.ElementAtOrDefault(0)  )
                from b_ in getValue(state, state.IP + 2, Mode.Position)
                from b in getValue(state, b_, modes.ElementAtOrDefault(1)  )
                select condition(a) ? state.WithIP(b) : state.WithIncrementIP(3);
        }

        public static Option<ProgramState> ExecExit(ProgramState state)
        => Some(state.WithStopping());

     
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

        public static Func<Mem, Mem, Mem> Add() => (a, b) => a + b;
        public static Func<Mem, Mem, Mem> Mul() => (a, b) => a * b;
        public static Func<Mem, Mem, Mem> Eq() => (a, b) => a == b ? 1 : 0;
        public static Func<Mem, Mem, Mem> Lt() => (a, b) => a < b ? 1 : 0;

        public static Func<Mem, bool> True() => x => x != 0;
        public static Func<Mem, bool> False() => x => x == 0;


        public static Func<OpCode, Func<ProgramState, Option<ProgramState>>> Dispatch()
        => opcode =>
        {
            switch (opcode)
            {
                case OpCode.Add:
                    return state => ProgramState.Exec(state, Add());

                case OpCode.Mul:
                    return state => ProgramState.Exec(state, Mul());

                case OpCode.Equals:
                    return state => ProgramState.Exec(state, Eq());

                case OpCode.LessThan:
                    return state => ProgramState.Exec(state, Lt());

                case OpCode.Input:
                    return state => ProgramState.ExecInput(state);

                case OpCode.Output:
                    return state => ProgramState.ExecOutput(state);

                case OpCode.JmpIfTrue:
                    return state => ProgramState.ExecJump(state, True());

                case OpCode.JmpIfFalse:
                    return state => ProgramState.ExecJump(state, False());

				case OpCode.Adjust: 
					return state => ProgramState.ExecAdjust(state);


                case OpCode.Exit:
                    return state => ProgramState.ExecExit(state);
            }
            return state => None;
        };

        private static Option<OpCode> ToOpCode(Mem value)
        {
            Mem opcode = value % 100;
            if (opcode == 1)
                return Some(OpCode.Add);
            else if (opcode == 2)
                return Some(OpCode.Mul);
            else if (opcode == 3)
                return Some(OpCode.Input);
            else if (opcode == 4)
                return Some(OpCode.Output);
            else if (opcode == 5)
                return Some(OpCode.JmpIfTrue);
            else if (opcode == 6)
                return Some(OpCode.JmpIfFalse);
            else if (opcode == 7)
                return Some(OpCode.LessThan);
            else if (opcode == 8)
                return Some(OpCode.Equals);
			else if (opcode == 9)
				return Some(OpCode.Adjust);
            else if (opcode == 99)
                return Some(OpCode.Exit);
            return None;
        }

		public static Mode ToMode(this Mem flags) 
		=> (Mode)((flags % 10) % 3);

		public static IEnumerable<Mode> ModesFromInstruction(this Mem instruction)
        => 3.PowersOf10()
			.Aggregate(ImmutableList<Mode>.Empty,
			(accu, current) => accu.Add((instruction / (current*100)).ToMode()));

        public static Func<ProgramState, Option<OpCode>> GetOpCode()
        => state
            => ToOpCode(state.Read(state.IP));

        public static Func<ProgramState, IEnumerable<Mode>> GetModeFlags()
        => state
            => state.Read(state.IP).ModesFromInstruction();

        public static Func<ProgramState, Mem, Mode, Option<Mem>> GetInt()
        => (state, raw, mode) => {
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


        public static Func<ProgramState, Adr, Mode, Mem, Option<ProgramState>> PutInt()
        => (state, raw, mode, value)
            => Some(state.WithProgram(
				state.Program.SetItem(
					raw + ((mode == Mode.Relative) ? state.RelativeBase:0), value)));

    }
}