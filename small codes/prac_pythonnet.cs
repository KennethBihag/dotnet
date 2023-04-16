using Python.Runtime;
#define PY_IMPORT
Console.WriteLine("DONE");
Console.ForegroundColor = ConsoleColor.Green;
#if PY_IMPORT
PythonEngine.PythonPath += $";D:\\KennethBihag\\MyDrive\\MyCodes\\python";
#endif
Console.WriteLine(PythonEngine.PythonPath); //PYTHONPATH env, need to add here the path to py script(s) to be run later
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine(PythonEngine.PythonHome); //py.exe, need not set if py.exe is alread in path env
Console.ResetColor();
#if PE_EXEC || PY_SCOPE
string sc = File.ReadAllText("D:\\KennethBihag\\MyDrive\\MyCodes\\python\\app.py"); //if not using the Py.Import method to run script
#endif

//PythonEngine.Initialize();
using (Py.GIL())
{
#if PY_IMPORT
   dynamic sc = Py.Import("app"); //directly run script as a module
   sc.Run();
#elif PE_EXEC
   PythonEngine.Exec(sc); // directly run script
#else
   PyScope scope = Py.CreateScope(); //when opting to use scope for running
   scope.Exec(sc);
   dynamic Run = scope.Get("Run");
   Run();
#endif
}
//PythonEngine.Shutdown();