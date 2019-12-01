
using System;

namespace advent.of.code
{

    // Env -> T (aka. Reader)
    public static class FuncExtensions
    {
        public static Func<Env, R> Map<Env, T, R>
           (this Func<Env, T> f, Func<T, R> g)
           => x => g(f(x));

        public static Func<Env, R> Bind<Env, T, R>
           (this Func<Env, T> f, Func<T, Func<Env, R>> g)
           => env => g(f(env))(env);

        // same as above, in uncurried form
        public static Func<Env, R> Bind<Env, T, R>
           (this Func<Env, T> f, Func<T, Env, R> g)
           => env => g(f(env), env);


        // LINQ

        public static Func<Env, R> Select<Env, T, R>(
			this Func<Env, T> f, Func<T, R> g) => f.Map(g);

        public static Func<Env, P> SelectMany<Env, T, R, P>(
			this Func<Env, T> f, Func<T, Func<Env, R>> bind,
			Func<T, R, P> project)
           => env =>
           {
               var t = f(env);
               var r = bind(t)(env);
               return project(t, r);
           };
    }
}