using System;

public class AlekseyPathLoader : System.Runtime.Loader.AssemblyLoadContext
{
	private string libPath;

	public AlekseyPathLoader (string libPath) {
		this.libPath = libPath;
	}

	protected override System.Reflection.Assembly Load (System.Reflection.AssemblyName aname)
	{
		var p = System.IO.Path.Combine (libPath, aname.Name + ".dll");
		Console.WriteLine ($"trying to find {aname.FullName} in '{p}'");
		return LoadFromAssemblyPath (p);
	}
}

public class AlekseyDelegateALC : System.Runtime.Loader.AssemblyLoadContext
{
	private System.Runtime.Loader.AssemblyLoadContext d;

	public AlekseyDelegateALC (System.Runtime.Loader.AssemblyLoadContext d) {
		Console.WriteLine ("Created a1");
		this.d = d;
	}

	protected override System.Reflection.Assembly Load (System.Reflection.AssemblyName aname)
	{
		Console.WriteLine ($"Delegating load of {aname.FullName}");
		try {
			return d.LoadFromAssemblyName (aname);
		} finally {
			Console.WriteLine ($"Returned from delegating load of {aname.FullName}");
		}
	}
}

public class Prog {
	public static void Main () {
		var defaultALC = System.Runtime.Loader.AssemblyLoadContext.Default;
		var p = /*System.Environment.CurrentDirectory*/ System.IO.Path.GetDirectoryName (typeof (Prog).Assembly.Location);
		var libPath = System.IO.Path.Combine (p, "subs");

		Console.WriteLine ($"the path to lib.dll is '{libPath}'");
		var a0 = new AlekseyPathLoader (libPath);

		var assm = a0.LoadFromAssemblyName (new System.Reflection.AssemblyName ("lib"));

		var a1 = new AlekseyDelegateALC (a0);

		var assm2 = a1.LoadFromAssemblyName (new System.Reflection.AssemblyName ("lib2"));

		var a2 = System.Runtime.Loader.AssemblyLoadContext.GetLoadContext (assm2);

		Console.WriteLine ($"a0 same as a2 ? {Object.ReferenceEquals (a0, a2)}");
		Console.WriteLine ($"a1 same as a2 ? {Object.ReferenceEquals (a1, a2)}");
		Console.WriteLine ($"a2 same as default? {Object.ReferenceEquals (a2, defaultALC)}");
	}
}
