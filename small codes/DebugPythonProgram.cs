using Python;
using Python.Runtime;
using Vertex = DummyDB<float>.Vertex;
using System.Diagnostics;


PythonEngine.PythonPath += ";D:\\KennethBihag\\MyDrive\\MyCodes\\python";
PythonEngine.Initialize();
#if PY_IMPORT
string runFromCS = @"
def run(p1):
   print(""Hello"")
   print(p1.X,"" "",p1.Y)

def GetPoint():
   print( String(1.23,3.14,-69) )

print(myVar)
";
#endif
using (Py.GIL())
{
   Vertex v1 = new();
   Console.WriteLine("Before Python loading : {0}", v1);
   /*
   //
   var scope = Py.CreateScope();
   scope.Set("p1",new Vertex());
   scope.Exec(runFromCS);
   */
   /*
   PythonEngine.Exec("myVar = (96,1,24)");
   PythonEngine.Exec(runFromCS);
   PythonEngine.Exec("GetPoint()");
   */
   dynamic RunFromCS = Py.Import("RunFromCS");
   RunFromCS.run(new Vertex());
   var t = RunFromCS.GetPoint();
   Console.WriteLine("Got point from Python : {0}",t);
   RunFromCS.CSToPy(v1);
   Console.WriteLine("After python: {0}", v1);
}
PythonEngine.Shutdown();