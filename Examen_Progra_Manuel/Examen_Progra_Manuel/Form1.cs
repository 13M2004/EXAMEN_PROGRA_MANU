using System;
using System.Windows.Forms;

namespace Examen_Progra_Manuel
{
    public partial class Form1 : Form
    {
     
        private string[] Nombre_Selecciones =
        {
            "Qatar",
            "Ecuador",
            "Senegal",
            "Paises Bajos",
            "Inglaterra",
            "Iran",
            "Estados Unidos",
            "Gales",
            "Argentina",
            "Arabia Saudita",
            "Mexico",
            "Polonia",
            "Francia",
            "Australia",
            "Dinamarca",
            "Tunez",
            "España",
            "Costa Rica",
            "Alemania",
            "Japon",
            "Belgica",
            "Canada",
            "Marruecos",
            "Croacia",
            "Brasil",
            "Serbia",
            "Suiza",
            "Camerun",
            "Portugal",
            "Ghana",
            "Uruguay",
            "Corea del Sur"
        };

      
        private Mundial seleccion;

        private void CargarSlecciones()
        {
            dataGridViewMundial.DataSource = seleccion.LeerSelecciones();

        }

        public Form1()
        {
            InitializeComponent();
            seleccion = new Mundial ("localhost", "root", "");
            this.Load += new System.EventHandler(this.Form1_Load);
        }

        // MOSTRAR LSITADO
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxSelecciones.Items.AddRange(Nombre_Selecciones);
        }


        //MOSTRAR PERSONAJES
        private void buttonCargar_Click(object sender, EventArgs e)
        {
            dataGridViewMundial.DataSource = seleccion.LeerSelecciones();
        }


        private void comboBoxGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSelecciones.Items.AddRange(Nombre_Selecciones);
        }


        //AGREGAR PERSONAJE
        private void buttonInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener valores de los controles de la interfaz de usuario               
                string nombre_selecciones = comboBoxSelecciones.Text;
                string Jugadores_Destacados = textBoxJugadores_Destacados.Text;
                string no_clasificaciones_mundial = textBoxNo_Clasificaciones_Mundial.Text;
                string frases_selecciones = textBoxFrases_Selecciones.Text;
                string fecha_ultimo_mundial_ganado = dateTimePickerFecha_Ultimo_Mundial_Ganado.Text;
                int valor_plantilla = (int)numericUpDownValor_Plantilla.Value;
           

                // Llamar al método CrearSeleccion y obtener la respuesta
                int respuesta = seleccion.CrearPersonaje(nombre_selecciones, Jugadores_Destacados, no_clasificaciones_mundial, frases_selecciones, fecha_ultimo_mundial_ganado, valor_plantilla);

                // Verificar la respuesta e informar al usuario
                if (respuesta > 0)
                {
                    MessageBox.Show("Personaje creado correctamente");
                    dataGridViewMundial.DataSource = seleccion.LeerPersonajes();
                }
                else
                {
                    MessageBox.Show("Error al crear el personaje");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y mostrar un mensaje de error al usuario
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }


        //ACTUALIZAR
        private void buttonActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que haya un registro seleccionado en el DataGridView
                if (dataGridViewMundial.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un personaje para actualizar.");
                    return;
                }

                // Obtener la fila seleccionada
                DataGridViewRow row = dataGridViewMundial.SelectedRows[0];

                // Obtener el ID del personaje (asumiendo que hay una columna "id")
                int id = Convert.ToInt32(row.Cells["id"].Value);

                // Obtener los valores de los controles de la interfaz de usuario
                string nombre_selecciones = comboBoxSelecciones.Text;
                string Jugadores_Destacados = textBoxJugadores_Destacados.Text;
                string no_clasificaciones_mundial = textBoxNo_Clasificaciones_Mundial.Text;
                string frases_selecciones = textBoxFrases_Selecciones.Text;
                string fecha_ultimo_mundial_ganado = dateTimePickerFecha_Ultimo_Mundial_Ganado.Text;
                int valor_plantilla = (int)numericUpDownValor_Plantilla.Value;

                // Validar los datos antes de la actualización
                if (string.IsNullOrWhiteSpace(nombre_selecciones) || string.IsNullOrWhiteSpace(Jugadores_Destacados) ||
                string.IsNullOrWhiteSpace(no_clasificaciones_mundial) || string.IsNullOrWhiteSpace(frases_selecciones) ||
                string.IsNullOrWhiteSpace(fecha_ultimo_mundial_ganado))
                {
                    MessageBox.Show("Uno o más parámetros son inválidos.");
                    return;
                }

                // Confirmar con el usuario si desea realizar la actualización
                DialogResult result = MessageBox.Show("¿Está seguro de que desea actualizar este personaje?", "Confirmar Actualización", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Llamar al método para actualizar el personaje
                    int respuesta = seleccion.ActualizarPersonaje(id, nombre_selecciones, Jugadores_Destacados, no_clasificaciones_mundial, frases_selecciones, fecha_ultimo_mundial_ganado, valor_plantilla);

                    // Verificar la respuesta del método de actualización
                    if (respuesta > 0)
                    {
                        MessageBox.Show("Personaje actualizado correctamente.");
                        // Actualizar el DataGridView para reflejar los cambios
                        dataGridViewMundial.DataSource = seleccion.LeerPersonajes();
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar el personaje.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y mostrar un mensaje de error al usuario
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }


        }


        //ELIMINAR SELECCCION
        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewMundial.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un personaje para eliminar.");
                    return;
                }

                DataGridViewRow row = dataGridViewMundial.SelectedRows[0];
                int id = Convert.ToInt32(row.Cells["id"].Value);

                var confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar esta SELECCIÓN?",
                                                     "Confirmar Eliminación",
                                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    int respuesta = seleccion.EliminarPersonaje(id);

                    if (respuesta > 0)
                    {
                        MessageBox.Show("Personaje eliminado correctamente");
                        CargarSlecciones();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el personaje");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }


    }
}
