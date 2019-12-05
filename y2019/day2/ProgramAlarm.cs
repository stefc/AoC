// http://adventofcode.com/2019/day/2

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;


using advent.of.code;


namespace advent.of.code.y2019.day2
{
	using static F;

	public readonly struct ProgramState {
		public readonly int PC;
		public readonly ImmutableArray<int> Program;

		public ProgramState(int pc, IEnumerable<int> program)
			: this(pc, program.Aggregate(ImmutableArray<int>.Empty,
					(accu,current) => accu.Add(current)))
		{}

		private ProgramState(int pc, ImmutableArray<int> program)
		{
			this.PC = pc;
			this.Program = program;
		}

		public int OpCode
			=> this.Program.ElementAtOrDefault(this.PC);

		public ProgramState WithSingleStep()
			=> new ProgramState(this.PC+4, this.Program);

		public ProgramState WithProgram(ImmutableArray<int> program)
			=> new ProgramState(this.PC, program);

	}

	public static class ProgramAlarm
	{

		public static ProgramState CreateProgram(IEnumerable<int> program)
			=> new ProgramState(0, program);

		public static ProgramState CreateProgram(params int[] program)
			=> new ProgramState(0, program);


	/*	public static StatefulComputation<int, ProgramState> ConstructStateMaschine()
		=> code => {
			return ((code != 99) ? ConstructStateMaschine()(code).Value : 0, opCode);
		};
*/
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

		public static Func<ProgramState,int, Option<int>> GetOpCode()
		=> (state,index)
			=> (index < state.Program.Count()) ?
				Some(state.Program.ElementAt(state.PC)) : None;

		public static Func<ProgramState,int,Option<int>> GetInt()
		=> (state, index)
			=> (index < state.Program.Count()) ?
				Some(state.Program.ElementAt(index)) : None;

		public static Func<ProgramState,int,int,ProgramState> PutInt()
		=> (state, index, value)
			=> state.WithProgram(state.Program.SetItem(index,value));

	}
}