namespace ejercicio_5
{
  internal class Program
  {
    static void Main(string[] args)
    {
      ProcesoDatos pd1 = new("Lab", "9.5", "iOS", "db_origen", "db_fin");
      ProcesoDatos pd2 = new("Prod", "6.4", "Windows", "db_entrada", "db_salida");

      ProcesoDatos[] bases1 = [pd1, pd2];

      foreach (ProcesoDatos bases in bases1)
      {
        bases.Bajar();
        bases.Status();
        bases.Levantar("db_entrada", "db_salida");
        bases.Levantar("db_origen", "db_fin");
        bases.FiltrarBD();
        bases.AlmacenarBD();
        bases.MostrarInfo();
        bases.Bajar();
        Console.WriteLine();
      }
      Aplicacion ap1 = new("Dev", "0.2", "Windows", "Python", "4.2", "d://app1");
      Aplicacion ap2 = new("Prod", "1.4", "Linux", "C#", "6.8", "f://app2");

      Aplicacion[] bases2 = [ap1, ap2];

      foreach (Aplicacion app in bases2)
      {
        app.Bajar();
        app.Levantar("Python", "4.2");
        app.Levantar("C#", "6.8");
        app.MostrarInfo();
        app.Bajar();
        Console.WriteLine();
      }
      Console.ReadLine();
    }

    abstract class Instancia
    {
      protected string Nombre { get; set; }
      protected string Version { get; set; }
      protected string SistemaOperativo { get; set; }
      protected bool Estado { get; set; }

      protected Instancia(string nombre, string version, string so)
      {
        Nombre = nombre;
        Version = version;
        SistemaOperativo = so;
        Estado = false; // false = "down" - true = "up"
      }

      public abstract void Levantar();

      public void Bajar()
      {
        if (Estado)
        {
          Estado = false;
          Console.WriteLine($"Cerrando instancia {Nombre}...");
        }
        else
        {
          Console.WriteLine("No hay una instancia activa.");
        }
      }
      public void Status()
      {
        Console.WriteLine($"Estado de la instancia {Nombre}: {(Estado ? "up" : "down")}");
      }

      public void MostrarInfo()
      {
        Console.WriteLine($"Maquina virtual: {Nombre} v{Version}\nSistema Operativo: {SistemaOperativo}");
        Status();
      }
    }

    class ProcesoDatos : Instancia
    {
      private string DatosOrigen { get; set; }
      private string DatosFin { get; set; }

      public ProcesoDatos(string nombre, string version, string so, string datosOrigen, string datosFin) : base(nombre, version, so)
      {
        DatosOrigen = datosOrigen;
        DatosFin = datosFin;
      }
      public override void Levantar() { }
      public void Levantar(string datosOrigen, string datosFin)
      {
        if (!Estado)
        {
          if (DatosOrigen == datosOrigen && DatosFin == datosFin)
          {
            Estado = true;
            Console.WriteLine($"Se levantó correctamente la instancia.\nPosee acceso a datos de origen almacenados {DatosOrigen} y a datos de fin almacenados {DatosFin}.");
          }
          else
          {
            Console.WriteLine("No se pudo levantar la instancia. Datos de origen y fin incorrectos.");
          }
        }
        else
        {
          Console.WriteLine("La instancia ya se encuentra activa.");
        }
      }

      public new void MostrarInfo()
      {
        base.MostrarInfo();
        Console.WriteLine($"Datos de origen: {DatosOrigen}\nDatos de fin: {DatosFin}");
      }

      public void ClonarBD()
      {
        if (Estado)
        {
          Console.WriteLine("Clonando Base de Datos...");
        }
      }
      public void FiltrarBD()
      {
        if (Estado)
        {
          Console.WriteLine("Filtrando Base de Datos...");
        }
      }
      public void AlmacenarBD()
      {
        if (Estado)
        {
          Console.WriteLine("Almacenando datos en Base de Datos...");
        }
      }
    }
    class Aplicacion : Instancia
    {
      private string LenguajeProg { get; set; }
      private string VersionLenguaje { get; set; }
      private string UrlBD { get; set; }

      public Aplicacion(string nombre, string version, string so, string lenguajeProg, string versionLenguaje, string urlBD) : base(nombre, version, so)
      {
        LenguajeProg = lenguajeProg;
        VersionLenguaje = versionLenguaje;
        UrlBD = urlBD;
      }
      public override void Levantar() { }
      public void Levantar(string lenguajeProg, string versionLenguaje)
      {
        if (!Estado)
        {
          if (LenguajeInstalado(lenguajeProg, versionLenguaje))
          {
            Estado = true;
            Console.WriteLine($"Se levantó correctamente la instancia {Nombre}.\nSe instaló correctamente el lenguaje {LenguajeProg} en la versión {VersionLenguaje} y posee acceso a la BD en {UrlBD}.");
          }
          else
          {
            Console.WriteLine($"No se pudo levantar la instancia. Lenguaje de programación {lenguajeProg} en la versión {versionLenguaje} no encontrado.");
          }
        }
        else
        {
          Console.WriteLine("La instancia ya se encuentra activa.");
        }
      }
      public bool LenguajeInstalado(string lenguajeProg, string versionLenguaje)
      {
        return (LenguajeProg == lenguajeProg && VersionLenguaje == versionLenguaje);
      }
      public new void MostrarInfo()
      {
        base.MostrarInfo();
        Console.WriteLine($"Lenguaje: {LenguajeProg} v{VersionLenguaje}");
      }
    }
  }
}
