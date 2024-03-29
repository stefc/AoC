namespace advent.of.code.y2020.day8
{
	public enum Operations
	{
		Acc,
		Jmp,
		Nop
	}

	public struct Instruction
	{

		public Operations Operation { get; init; }
		public int Argument { get; init; }
	}


	// http://adventofcode.com/2020/day/8
	static class HandheldHalting
	{

		const string pattern = @"(?'operation'acc|jmp|nop) (?'argument'[+|-]\d+)";

		public static int GetAccumulator(IEnumerable<string> lines)
		{
			var program = new ProgramState(lines.Select(FromString));
			return program.GetAccumulator();
		}

		public static int GetAccumulatorFix(IEnumerable<string> lines)
		{
			var prg = lines.Select(FromString).ToImmutableList();

			var accus = prg
				.Select( (instruction,idx) => {

					if (instruction.Operation == Operations.Acc)
					{
						return prg;
					}
					else {
						var newInstruction = new Instruction()
						{
							Operation = instruction.Operation == Operations.Jmp ? Operations.Nop : Operations.Jmp,
							Argument = instruction.Argument
						};

						return prg.RemoveAt(idx).Insert(idx, newInstruction);
					}
				})
				.Select( onePrg => new ProgramState(onePrg).GetAccuNoneInfinite());

			return accus.First( x => x.HasValue ).Value;

		}


		static Instruction FromString(this string line)
		{
			RegexOptions options = RegexOptions.IgnoreCase;
			Match match = Regex.Matches(line, pattern, options).First();

			var operation = match.Groups["operation"].Value;
			var argument = match.Groups["argument"].Value;

			return new Instruction() {
				Operation = (Operations) Enum.Parse(typeof(Operations), operation, true),
				Argument = Convert.ToInt32(argument) };
		}

	}

	public readonly struct ProgramState
    {
        public int IP { get; init; }

		public int Accu { get; init; }
		public readonly ImmutableList<Instruction> program;

		private readonly ImmutableHashSet<int> visits;


        public ProgramState(IEnumerable<Instruction> program): this(
			ImmutableList<Instruction>.Empty.AddRange(program), 0, 0, ImmutableHashSet<int>.Empty)
        { }

        private ProgramState(ImmutableList<Instruction> program, int ip, int accu, ImmutableHashSet<int> visits)
        {
            this.IP = ip;
            this.Accu = accu;
			this.program = program;
			this.visits = visits;
        }

		public int GetAccumulator() {

			var state = this;
			while (!state.visits.Contains(state.IP) && (state.IP < state.program.Count))
			{
				state = this.Dispatch(state);
			}
			return state.Accu;
		}

		public int? GetAccuNoneInfinite() {
			var state = this;
			while (!state.visits.Contains(state.IP) && (state.IP >= 0 && state.IP < state.program.Count))
			{
				state = this.Dispatch(state);
			}
			return (state.IP < 0 || state.IP >= state.program.Count) ? state.Accu : null;
		}

		public ProgramState WithIP(int ip)
            => new ProgramState(this.program, ip, this.Accu, this.visits.Add(this.IP));

		public ProgramState WithAccu(int accu)
            => new ProgramState(this.program, this.IP, accu, this.visits);

		public ProgramState Dispatch(ProgramState state)
        {
			var instruction =state.program[state.IP];
            var opCode = instruction.Operation;
            switch (opCode)
            {
                case Operations.Nop:
                    return state.WithIP(state.IP+1);

                case Operations.Acc:
                    return state.WithAccu(state.Accu + instruction.Argument).WithIP(state.IP+1);

                case Operations.Jmp:
                    return state.WithIP(state.IP + instruction.Argument);
            }
            return state;
        }
    }
}
