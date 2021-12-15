using System.Reflection;

namespace advent.of.code.common;

public class TraitFinder {

   
	public record struct YearDay (int Year, int Day) {

	}

	// private YearDay GetTraitForType(Type t) {
	// 	var cads = CustomAttributeData.GetCustomAttributes(t);

	// 	var daycad = cads.SingleOrDefault( cad =>  )
	// } 

	private bool HasYearAndDay(Type t) {

		var cads = CustomAttributeData.GetCustomAttributes(t);

		var args = cads.SelectMany( cad => cad.ConstructorArguments.OfType<CustomAttributeTypedArgument>());
		return 
			args.Any( arg => (arg.ArgumentType == typeof(String)) && arg.Value.Equals("Day"))
				&& 
			args.Any( arg => (arg.ArgumentType == typeof(String)) && arg.Value.Equals("Year"));
	}

	private int? GetDay( CustomAttributeData cad ) {
		var args = cad.ConstructorArguments.OfType<CustomAttributeTypedArgument>().ToArray();
		var arg1 = args.First();
		var arg2 = args.Last();
		if ((arg1.ArgumentType == typeof(String)) && (arg1.Value.Equals("Day"))) 
		{
			if (arg2.ArgumentType == typeof(String))
			{
				return Convert.ToInt32(arg2.Value);
			}
		}
		return null;
	}

	private int? GetYear( CustomAttributeData cad ) {
		var args = cad.ConstructorArguments.OfType<CustomAttributeTypedArgument>().ToArray();
		var arg1 = args.First();
		var arg2 = args.Last();
		if ((arg1.ArgumentType == typeof(String)) && (arg1.Value.Equals("Year"))) 
		{
			if (arg2.ArgumentType == typeof(String))
			{
				return Convert.ToInt32(arg2.Value);
			}
		}
		return null;
	}

	private IEnumerable<Type> GetAllTypesThatImplementIPuzzleTest()
    => System.Reflection.Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(type => typeof(IPuzzleTest).IsAssignableFrom(type) 
            && !type.IsInterface 
            && Attribute.IsDefined(type, typeof(TraitAttribute)));

}