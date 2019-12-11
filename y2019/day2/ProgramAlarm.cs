// http://adventofcode.com/2019/day/2

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace advent.of.code.y2019.day2
{
    using static F;

    using Computation = StatefulComputation<ProgramState, ImmutableArray<int>>;

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

		public int OpCode
			=> this.Program.ElementAtOrDefault(this.IP);

		public ProgramState WithIncrementIP(int step = 4)
			=> new ProgramState(this.IP+step, this.Program);

		public ProgramState WithProgram(ImmutableArray<int> program)
			=> new ProgramState(this.IP, program);

		public ProgramState WithExecute() {
			var getValue = ProgramAlarm.GetInt();
			var putValue = ProgramAlarm.PutInt();
			var getOpCode = ProgramAlarm.GetOpCode();
			var getFunc = ProgramAlarm.Dispatch();

			var state = this;

			var newState =
				from opCode in getOpCode(this)
				from f in getFunc(opCode)
				from a_ in getValue(state, state.IP+1)
				from a in getValue(state, a_)
				from b_ in getValue(state, state.IP+2)
				from b in getValue(state, b_)
				from ptr in getValue(state, state.IP+3)
				select putValue(state,ptr,f(a,b));

			return newState.GetOrElse(state).WithIncrementIP();
		}
	}

	public static class ProgramAlarm
	{

		public static ProgramState CreateProgram(IEnumerable<int> program)
			=> new ProgramState(0, program);

		public static ProgramState CreateProgram(params int[] program)
			=> new ProgramState(0, program);


		public static Computation CreateStateMaschine()
		=> state => (
				(state.OpCode != 99) ?
				CreateStateMaschine()(state.WithExecute()).Value
				:
				state.Program,
				state);

		public static Func<int,int,int> Add() => (a,b) => a+b;
		public static Func<int,int,int> Mul() => (a,b) => a*b;

		public static Func<int,Option<Func<int,int,int>>> Dispatch()
		=> opcode => {
			switch (opcode)
			{
				case 1:
					return Some(Add());
				case 2:
					return Some(Mul());
			}
			return None;
		};

		public static Func<ProgramState, Option<int>> GetOpCode()
		=> state
			=> (state.IP < state.Program.Count()) ?
				Some(state.Program.ElementAt(state.IP)) : None;

		public static Func<ProgramState,int,Option<int>> GetInt()
		=> (state, index)
			=> (index < state.Program.Count()) ?
				Some(state.Program.ElementAt(index)) : None;

		public static Func<ProgramState,int,int,ProgramState> PutInt()
		=> (state, index, value)
			=> state.WithProgram(state.Program.SetItem(index,value));

	}
}