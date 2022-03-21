using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5KeithleenR
{
    public partial class Form1 : Form
    {
        List<Empleado> empleados = new List<Empleado>();
        List<Asistenciacs> asistencias = new List<Asistenciacs>();
        List<Sueldo> sueldos = new List<Sueldo>();

        public Form1()
        {
            InitializeComponent();
        }

        void CargarEmpleado()
        {
            FileStream stream = new FileStream("Empleados.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Empleado empleado = new Empleado();
                empleado.NoEmpleado = Convert.ToInt16(reader.ReadLine());
                empleado.Nombre = reader.ReadLine();
                empleado.SueldoxHora = Convert.ToDecimal(reader.ReadLine());
                empleados.Add(empleado);
            }
            reader.Close();
        }

        void CargarAsistencia()
        {
            FileStream stream = new FileStream("Asistencia.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Asistenciacs asistencia = new Asistenciacs();
                asistencia.NoEmpleado = Convert.ToInt32(reader.ReadLine());
                asistencia.HorasMes = Convert.ToInt32(reader.ReadLine());
                asistencia.Mes = reader.ReadLine();
                asistencias.Add(asistencia);
            }

            reader.Close();
        }

        void CargarGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = empleados;
            dataGridView1.Refresh();

            dataGridView2.DataSource = null;
            dataGridView2.Refresh();
            dataGridView2.DataSource = asistencias;
            dataGridView2.Refresh();

        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            CargarAsistencia();
            CargarEmpleado();
            CargarGrid();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < empleados.Count; i++)
            {
                for (int j = 0; j < asistencias.Count; j++)
                {
                    if (empleados[i].NoEmpleado == asistencias[j].NoEmpleado)
                    {
                        Sueldo sueldo = new Sueldo();
                        sueldo.Nombre = empleados[i].Nombre;
                        sueldo.NoEmpleado = empleados[i].NoEmpleado;
                        sueldo.SueldoMes = empleados[i].SueldoxHora * asistencias[j].HorasMes;
                        sueldo.Mes = asistencias[j].Mes;
                        sueldos.Add(sueldo);
                    }
                }
            }

            dataGridView3.DataSource = null;
            dataGridView3.Refresh();
            dataGridView3.DataSource = sueldos;
            dataGridView3.Refresh();

        }
    }
}
