using A_Test_EF_Core;

{
    _ = new Seed();
    GC.Collect();
    GC.WaitForPendingFinalizers();
    GC.WaitForFullGCComplete();
    GC.Collect();

}

Console.ReadLine();

Console.WriteLine("Hello, World!");

