using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integracion53
{
	public class Persona
	{
		protected int _registroPersona;
		
		private string _nombre;
		private string _apellido;
		private long _telefono;
		

		/* Un descriptor de acceso que no tiene set solo puede escribirse una vez por el contructor 
		   Ejemplo
		   public Persona (int dni)
			{
				Dni = dni
			}
		 */
		
		public string Nombre
		{
			get { return this._nombre; }
			set { this._nombre = value; }
		}

		public string Apellido
		{
			get { return this._apellido; }
			set { this._apellido = value; }
		}
		public long Telefono
		{
			get { return this._telefono; }
			set { this._telefono = value; }
		}
		
		public static int registroPersona = 1;

	

		public Persona(string nombre, string apellido, long telefono)
		{
			
			this._nombre = nombre;
			this._apellido = apellido;
			this._telefono = telefono;
			
			this._registroPersona = registroPersona;
			registroPersona++;
		}
	}
}
