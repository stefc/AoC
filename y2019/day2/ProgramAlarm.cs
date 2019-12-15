// http://adventofcode.com/2019/day/2

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace advent.of.code.y2019.day2
{
    using static F;

    using Computation = StatefulComputation<Option<ProgramState>, ImmutableArray<int>>;

	public enum OpCode {
		Add = 1,
		Mul = 2,

		Input = 3,
		Output = 4,

		JmpIfTrue = 5,

		JmpIfFalse = 6,

		LessThan = 7,

		Equals = 8,

		Exit = 99,
	}

    public readonly struct ProgramState {
		public readonly int IP;
		public readonly ImmutableArray<int> Program;

		public readonly int Input; 

		public readonly ImmutableStack<int> Output;

		public ProgramState(int pc, IEnumerable<int> program, int input = 0) : 
		this(pc, 
			program.Aggregate(
			ImmutableArray<int>.Empty, (accu,current) => accu.Add(current)), 
			input, ImmutableStack<int>.Empty)
		{}

		private ProgramState(int ip, ImmutableArray<int> program, 
			int input, ImmutableStack<int> output)
		{
			this.IP = ip;
			this.Program = program;
			this.Input = input;
			this.Output = output;
		}

		public Option<OpCode> OpCode => ProgramAlarm.GetOpCode()(this);

		public ProgramState WithIncrementIP(int step = 4)
			=> new ProgramState(this.IP+step, this.Program, this.Input, this.Output);

		public ProgramState WithIP(int ip)
			=> new ProgramState(ip, this.Program, this.Input, this.Output);


		public ProgramState WithProgram(ImmutableArray<int> program)
			=> new ProgramState(this.IP, program, this.Input, this.Output);
		public ProgramState WithOutput(int output)
			=> new ProgramState(this.IP, this.Program, this.Input, 
				this.Output.Push(output));

		public ProgramState WithExecute() {
			var getOpCode = ProgramAlarm.GetOpCode();
			var getFunc = ProgramAlarm.Dispatch();
			var state = this;
			return (				
				from code in getOpCode(state)
				from newState in getFunc(code)(state)
				select newState).GetOrElse(state);
		}

		public static Option<ProgramState> Exec(ProgramState state, 
			Func<int,int,int> operation) {
			var getValue = ProgramAlarm.GetInt();
			var putValue = ProgramAlarm.PutInt();
			var mode = ProgramAlarm.GetModeFlags()(state);

			bool immediate1st = mode % 2 == 1;
			bool immediate2nd = (mode / 10) % 2 == 1;
			// bool immediate3rd = (mode / 100) % 2 == 1;

			return 
				from a_ in getValue(state, state.IP+1)
				from a in immediate1st ? Some(a_) : getValue(state, a_)
				from b_ in getValue(state, state.IP+2)
				from b in immediate2nd ? Some(b_) : getValue(state, b_)
				from ptr in getValue(state, state.IP+3)
				from newState in putValue(state,ptr,operation(a,b))
				select newState.WithIncrementIP();
		}

		public static Option<ProgramState> ExecInput(ProgramState state) {
			var getValue = ProgramAlarm.GetInt();
			var putValue = ProgramAlarm.PutInt();
			return 
				from ptr in getValue(state, state.IP+1)
				from newState in putValue(state,ptr,state.Input)
				select newState.WithIncrementIP(2);
		}

		public static Option<ProgramState> ExecOutput(ProgramState state) {
			var getValue = ProgramAlarm.GetInt();
			var putValue = ProgramAlarm.PutInt();
			var mode = ProgramAlarm.GetModeFlags()(state);

			bool immediate1st = mode % 2 == 1;
			return 
				from a_ in getValue(state, state.IP+1)
				from a in immediate1st ? Some(a_) : getValue(state, a_)
				select state.WithOutput(a).WithIncrementIP(2);
		}

		public static Option<ProgramState> ExecJump(ProgramState state, 
			Func<int,bool> condition) {
			var getValue = ProgramAlarm.GetInt();
			var putValue = ProgramAlarm.PutInt();
			var mode = ProgramAlarm.GetModeFlags()(state);

			bool immediate1st = mode % 2 == 1;
			bool immediate2nd = (mode / 10) % 2 == 1;
			return 
				from a_ in getValue(state, state.IP+1)
				from a in immediate1st ? Some(a_) : getValue(state, a_)
				from b_ in getValue(state, state.IP+2)
				from b in immediate2nd ? Some(b_) : getValue(state, b_)
				select condition(a) ? state.WithIP(b) : state.WithIncrementIP(3);
		}
	}

	public static class ProgramAlarm
	{

		public static ProgramState CreateProgram(IEnumerable<int> program,
			int input = 0)
			=> new ProgramState(0, program, input);

		public static ProgramState CreateProgram(params int[] program)
			=> new ProgramState(0, program);


		public static Computation CreateStateMaschine()
		=> state =>
				state.Match(
					None: ()=> (ImmutableArray<int>.Empty, None),
					Some: s => Exec(s)
				);
				
		public static (ImmutableArray<int> Value, Option<ProgramState> State) Exec(
			ProgramState state) 
		{
			return 
				state.OpCode.Match(
					None: () => (Value: ImmutableArray<int>.Empty, State: Some(state)),
					Some: code => 
						code == OpCode.Exit ? 
						(Value: state.Program, State: Some(state)) 
						:
						CreateStateMaschine()(state.WithExecute())
						);
		}

		public static Func<int,int,int> Add() => (a,b) => a+b;
		public static Func<int,int,int> Mul() => (a,b) => a*b;
		public static Func<int,int,int> Eq() => (a,b) => a==b ? 1:0;
		public static Func<int,int,int> Lt() => (a,b) => a<b ? 1:0;


		public static Func<OpCode,Func<ProgramState,Option<ProgramState>>> Dispatch()
		=> opcode => {
			switch (opcode)
			{
				case OpCode.Add:
					return state => ProgramState.Exec(state,Add());

				case OpCode.Mul:
					return state => ProgramState.Exec(state,Mul());

				case OpCode.Equals:
					return state => ProgramState.Exec(state,Eq());

				case OpCode.LessThan:
					return state => ProgramState.Exec(state,Lt());

				case OpCode.Input:
					return state => ProgramState.ExecInput(state);

				case OpCode.Output:
					return state => ProgramState.ExecOutput(state);

				case OpCode.JmpIfTrue: 
					return state => ProgramState.ExecJump(state, x=>x!=0);
					
				case OpCode.JmpIfFalse: 
					return state => ProgramState.ExecJump(state, x=>x==0);


				case OpCode.Exit: 
					return state => Some(state);
			}
			return state => None;
		};

		private static Option<OpCode> ToOpCode(int value) {
			int opcode = value % 100;
			if (opcode ==1)
				return Some(OpCode.Add);
			else if (opcode==2) 
				return Some(OpCode.Mul);
			else if (opcode==3)
				return Some(OpCode.Input);
			else if (opcode==4)
				return Some(OpCode.Output);
			else if (opcode==5)
				return Some(OpCode.JmpIfTrue);
			else if (opcode==6)
				return Some(OpCode.JmpIfFalse);
			else if (opcode==7)
				return Some(OpCode.LessThan);
			else if (opcode==8)
				return Some(OpCode.Equals);
			else if (opcode==99)
				return Some(OpCode.Exit);
			return None;
		}

		public static Func<ProgramState, Option<OpCode>> GetOpCode()
		=> state
			=> (state.IP < state.Program.Count()) ?
				ToOpCode(state.Program.ElementAt(state.IP)) : None;

		public static Func<ProgramState, int> GetModeFlags()
		=> state
			=> (state.IP < state.Program.Count()) ?
				state.Program.ElementAt(state.IP) / 100 : 0;

		public static Func<ProgramState,int,Option<int>> GetInt()
		=> (state, index)
			=> (index < state.Program.Count()) ?
				Some(state.Program.ElementAt(index)) : None;

		public static Func<ProgramState,int,int,Option<ProgramState>> PutInt()
		=> (state, index, value)
			=> Some(state.WithProgram(state.Program.SetItem(index,value)));

	}
}