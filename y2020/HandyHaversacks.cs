using System.Linq;
using System.Collections.Generic;
using Combinatorics.Collections;
using System.Collections.Immutable;
using System;

namespace advent.of.code.y2020.day7
{

	internal enum Color
	{

		Red,
		White,
		Yellow,

		Orange,

		Gold,

		Blue,

		Olive,
		Black,

		Plum
	}

	internal enum ColorType
	{
		Light,

		Dark,

		Muted,

		Bright,

		Shiny,

		Faded,
		Vibrant,

		Dotted
	}

	// http://adventofcode.com/2020/day/7

	static class HandyHaversacks
	{

		public static Color ColorFromString(this string value)
			=> (Color)Enum.Parse(typeof(Color), value, true);

		public static ColorType ColorTypeFromString(this string value)
			=> (ColorType)Enum.Parse(typeof(ColorType), value, true);
	}
}
