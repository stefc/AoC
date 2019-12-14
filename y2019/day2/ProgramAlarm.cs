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

		Exit = 99,
	}

    public readonly struct ProgramState {
		public readonly int IP;
		public readonly ImmutableArray<int> Program;

		public ProgramState(int pc, IEnumerable<int> program)
			: this(pc, program.Aggregate(ImmutableArray<int>.Empty,
					(accu,current) => accu.Add(current)))
		{}

		private ProgramState(int ip, ImmutableArray<int> program)
		{
			this.IP = ip;
			this.Program = program;
		}

		public Option<OpCode> OpCode => ProgramAlarm.GetOpCode()(this);

		public ProgramState WithIncrementIP(int step = 4)
			=> new ProgramState(this.IP+step, this.Program);

		public ProgramState WithProgram(ImmutableArray<int> program)
			=> new ProgramState(this.IP, program);

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
	}

	public static class ProgramAlarm
	{

		public static ProgramState CreateProgram(IEnumerable<int> program)
			=> new ProgramState(0, program);

		public static ProgramState CreateProgram(params int[] program)
			=> new ProgramState(0, program);


		public static Computation CreateStateMaschine()
		=> state =>
				state.Match(
					None: ()=> (ImmutableArray<int>.Empty, None),
					Some: s => 
						(Value:
							s.OpCode.Match(
								None: () => ImmutableArray<int>.Empty, 
								Some: code => code == OpCode.Exit ? 
									s.Program 
									:
									CreateStateMaschine()(s.WithExecute()).Value
							),
						State: Some(s))
				);

		public static Func<int,int,int> Add() => (a,b) => a+b;
		public static Func<int,int,int> Mul() => (a,b) => a*b;


		public static Func<OpCode,Func<ProgramState,Option<ProgramState>>> Dispatch()
		=> opcode => {
			switch (opcode)
			{
				case OpCode.Add:
					return state => ProgramState.Exec(state,Add());

				case OpCode.Mul:
					return state => ProgramState.Exec(state,Mul());

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