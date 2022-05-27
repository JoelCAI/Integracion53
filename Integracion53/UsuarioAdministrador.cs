using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Integracion53
{
	public class UsuarioAdministrador : Usuario
	{
		protected List<Persona> _persona;

		public List<Persona> Persona
		{
			get { return this._persona; }
			set { this._persona = value; }
		}

		public UsuarioAdministrador(string nombre, List<Persona> persona) : base(nombre)
		{
			this._persona = persona;
		}


		public void MenuAdministrador(List<Persona> persona)
		{

			Persona = persona; /* Se reciben las personas del sistema */

			int opcion;
			do
			{

				Console.Clear();
				Console.WriteLine(" Bienvenido Usuario: *" + Nombre + "* ");
				opcion = Validador.PedirIntMenu("\n Menú de Registro de nuevas Personas: " +
									   "\n [1] Ingresar Datos" +
									   "\n [2] Buscar por Apellido" +
									   "\n [3] Buscar por Nombre" +
									   "\n [4] Grabar Datos" +
									   "\n [5] Leer Datos." +
									   "\n [6] Salir del Sistema.", 1, 5);

				switch (opcion)
				{
					case 1:
						DarAltaPersona();
						break;
					case 2:
						BuscarPersonaPorApellido();
						break;
					case 3:
						BuscarPersonaPorNombre();
						break;
					case 4:
						GrabarPersonaAgenda();
						break;
					case 5:
						LeerAgenda();
						break;
				}
			} while (opcion != 6);
		}

		public int BuscarPersonaNombre(string nombre)
		{
			for (int i = 0; i < this._persona.Count; i++)
			{
				if (this._persona[i].Nombre == nombre)
				{
					return i;
				}
			}
			/* si no encuentro el producto retorno una posición invalida */
			return -1;
		}

		public int BuscarPersonaApellido(string apellido)
		{
			for (int i = 0; i < this._persona.Count; i++)
			{
				if (this._persona[i].Apellido == apellido)
				{
					return i;
				}
			}
			/* si no encuentro el producto retorno una posición invalida */
			return -1;
		}

		public void BuscarPersonaPorNombre()
		{
			Console.Clear();
			string nombre;
			nombre = Validador.ValidarStringNoVacioUsuarioClave("\n Ingrese el Nombre de la Persona a Buscar. ");

			if (BuscarPersonaNombre(nombre) != -1)
			{
				VerPersona();
				Console.WriteLine("\n Usted digitó el Nombre *" + nombre + "*");
				Console.WriteLine("\n Como puede ver si existe una persona con ese Nombre");
				Validador.VolverMenu();
			}
			else
			{
				VerPersona();
				Console.WriteLine("\n Usted digitó el Nombre *" + nombre + "*");
				Console.WriteLine("\n Como puede ver no existe una persona con ese Nombre");
				Validador.VolverMenu();
			}
			
		}

		public void BuscarPersonaPorApellido()
		{
			string apellido;
			apellido = Validador.ValidarStringNoVacioUsuarioClave("\n Ingrese el Nombre de la Persona a Buscar. ");

			if (BuscarPersonaApellido(apellido) != -1)
			{
				VerPersona();
				Console.WriteLine("\n Usted digitó el Apellido *" + apellido + "*");
				Console.WriteLine("\n Como puede ver si existe una persona con ese Apellido");
				Validador.VolverMenu();
			}
			else
			{
				VerPersona();
				Console.WriteLine("\n Usted digitó el Apellido *" + apellido + "*");
				Console.WriteLine("\n Como puede ver no existe una persona con ese Apellido");
				Validador.VolverMenu();
			}

		}


		Dictionary<string, Persona> personaAgenda = new Dictionary<string, Persona>();

		protected override void DarAltaPersona()
		{
			

			string nombre;
			string apellido;
			long telefono;
			
			string opcion;

			Console.Clear();
			nombre = Validador.ValidarStringNoVacioUsuarioClave("\n Ingrese el Nombre de la Persona a agregar. ");
			if (BuscarPersonaNombre(nombre) == -1)
			{
				VerPersona();
				Console.WriteLine("\n ¡En hora buena! Puede utilizar este Nombre para crear una Persona Nueva en su agenda");
				apellido = ValidarStringNoVacioNombre("\n Ingrese el apellido de la Persona");
				Console.Clear();
				telefono = Validador.PedirLongMenu("\n Ingrese el télefono de la Persona. El valor debe ser entre ", 1100000000, 1199999999);
				



				opcion = ValidarSioNoPersonaNoCreada("\n Está seguro que desea crear esta Persona? ", nombre, apellido, telefono);

				if (opcion == "SI")
				{
					Persona p = new Persona(nombre, apellido, telefono);
					AddPersona(p);
					personaAgenda.Add(nombre, p);
					VerPersona();
					VerPersonaDiccionario();
					Console.WriteLine("\n Persona con Nombre *" + nombre + "* agregado exitósamente");
					Validador.VolverMenu();
				}
				else
				{
					VerPersona();
					Console.WriteLine("\n Como puede verificar no se creo ninguna Persona");
					Validador.VolverMenu();

				}

			}
			else
			{
				VerPersona();
				Console.WriteLine("\n Usted digitó el Nombre *" + nombre + "*");
				Console.WriteLine("\n Ya existe una persona con ese DNI");
				Console.WriteLine("\n Será direccionado nuevamente al Menú para que lo realice correctamente");
				Validador.VolverMenu();

			}

		}

		public void AddPersona(Persona persona)
		{
			this._persona.Add(persona);
		}

		public void RemoverPersona(int pos)
		{
			this._persona.RemoveAt(pos);
		}

		
		protected override void GrabarPersonaAgenda()
		{
			using (var archivoAgenda = new FileStream("archivoAgenda.txt", FileMode.Create))
			{
				using (var archivoEscrituraAgenda = new StreamWriter(archivoAgenda))
				{
					foreach (var persona in personaAgenda.Values)
					{

						var linea = 
									"\n Nombre de la Persona: " + persona.Nombre +
									"\n Apellido de la Persona: " + persona.Apellido +
									"\n Teléfono de la Persona: " + persona.Telefono;

						archivoEscrituraAgenda.WriteLine(linea);

					}

				}
			}
			VerPersona();
			Console.WriteLine("Se ha grabado los datos de las personas en la Agenda correctamente");
			Validador.VolverMenu();

		}

		protected override void LeerAgenda()
		{
			Console.Clear();
			Console.WriteLine("\n Personas en la agenda: ");
			using (var archivoAgenda = new FileStream("archivoAgenda.txt", FileMode.Open))
			{
				using (var archivoLecturaAgenda = new StreamReader(archivoAgenda))
				{
					foreach (var persona in personaAgenda.Values)
					{


						Console.WriteLine(archivoLecturaAgenda.ReadToEnd());


					}

				}
			}
			Validador.VolverMenu();

		}

		
		protected string ValidarSioNoPersonaNoCreada(string mensaje, string nombre, string apellido, long telefono)
		{

			string opcion;
			bool valido = false;
			string mensajeValidador = "\n Si esta seguro de ello escriba *" + "si" + "* sin los asteriscos" +
									  "\n De lo contrario escriba " + "*" + "no" + "* sin los asteriscos";
			string mensajeError = "\n Por favor ingrese el valor solicitado y que no sea vacio. ";

			do
			{
				VerPersona();

				Console.WriteLine(
								  "\n Nombre de la Persona a Crear: " + nombre +
								  "\n Apellido de la Persona a Crear: " + apellido +
								  "\n Teléfono de la Persona a Crear: " + telefono);

				Console.WriteLine(mensaje);
				Console.WriteLine(mensajeError);
				Console.WriteLine(mensajeValidador);
				opcion = Console.ReadLine().ToUpper();
				string opcionC = "SI";
				string opcionD = "NO";

				if (opcion == "" || (opcion != opcionC) & (opcion != opcionD))
				{
					continue;

				}
				else
				{
					valido = true;
				}

			} while (!valido);

			return opcion;
		}


		protected string ValidarStringNoVacioNombre(string mensaje)
		{

			string opcion;
			bool valido = false;
			string mensajeValidador = "\n Por favor ingrese el valor solicitado y que no sea vacio.";


			do
			{
				VerPersona();
				Console.WriteLine(mensaje);
				Console.WriteLine(mensajeValidador);

				opcion = Console.ReadLine().ToUpper();

				if (opcion == "")
				{

					Console.Clear();
					Console.WriteLine("\n");
					Console.WriteLine(mensajeValidador);

				}
				else
				{
					valido = true;
				}

			} while (!valido);

			return opcion;
		}

		public void VerPersona()
		{
			Console.Clear();
			Console.WriteLine("\n Personas en Agenda");
			Console.WriteLine(" #\t\tNombre.\t\tApellido.\t\tTelefono.");
			for (int i = 0; i < Persona.Count; i++)
			{
				Console.Write(" " + (i + 1));
				
				Console.Write("\t\t");
				Console.Write(Persona[i].Nombre);
				Console.Write("\t\t");
				Console.Write(Persona[i].Apellido);
				Console.Write("\t\t");
				Console.Write(Persona[i].Telefono);
				Console.Write("\t\t");
				
				Console.Write("\n");
			}

		}

		public void VerPersonaDiccionario()
		{
			Console.WriteLine("\n Personas en el Diccionario");
			for (int i = 0; i < personaAgenda.Count; i++)
			{
				KeyValuePair<string, Persona> persona = personaAgenda.ElementAt(i);

				Console.WriteLine("\n Nombre: " + persona.Key);
				Persona personaValor = persona.Value;

				
				Console.WriteLine(" Apellido Persona: " + personaValor.Apellido);
				Console.WriteLine(" Telefono Persona: " + personaValor.Telefono);
				

			}


		}





	}
}