using System;
using System.Globalization;
using MySql.Data.MySqlClient;

class Program
{
    static string connectionString = "Server=localhost;Database=biblioteca;Uid=root;Pwd=;";

    static void Main(string[] args)
    {
        //CrearTablaUsuarios();
        //CrearTablaGenerosLibro();
        //CrearTablaLibros();
        //CrearTablaPrestamos();
        while (true)
        {
            MostrarMenuPrincipal();
        }

    }

    static void CrearTablaUsuarios()
    {
        string query = @"
        CREATE TABLE IF NOT EXISTS usuarios (
            id INT NOT NULL AUTO_INCREMENT,
            nombre_completo VARCHAR(25) NOT NULL,
            dni VARCHAR(8) NOT NULL UNIQUE,
            telefono VARCHAR(16) NOT NULL UNIQUE,
            email VARCHAR(60) NOT NULL UNIQUE,
            creado_el TIMESTAMP DEFAULT NOW(),
            actualizado_el TIMESTAMP DEFAULT NOW(),
            estado TINYINT DEFAULT 1,
            PRIMARY KEY(id)
        );";

        EjecutarConsulta(query);
    }

    static void CrearTablaGenerosLibro()
    {
        string query = @"
        CREATE TABLE IF NOT EXISTS generos_libro (
            id INT NOT NULL AUTO_INCREMENT,
            genero VARCHAR(20) NOT NULL,
            PRIMARY KEY(id)
        );";

        EjecutarConsulta(query);
    }

    static void CrearTablaLibros()
    {
        string query = @"
        CREATE TABLE IF NOT EXISTS libros (
            id INT NOT NULL AUTO_INCREMENT,
            nombre_libro VARCHAR(40) NOT NULL,
            autor VARCHAR(25),
            fecha_lanzamiento VARCHAR(10) NOT NULL,
            id_genero INT NOT NULL,
            creado_el TIMESTAMP DEFAULT NOW(),
            actualizado_el TIMESTAMP DEFAULT NOW(),
            estado TINYINT DEFAULT 1,
            PRIMARY KEY(id),
            CONSTRAINT fk_generos FOREIGN KEY (id_genero) REFERENCES generos_libro(id)
        );";

        EjecutarConsulta(query);
    }

    static void CrearTablaPrestamos()
    {
        string query = @"
        CREATE TABLE IF NOT EXISTS prestamos (
            id INT NOT NULL AUTO_INCREMENT,
            usuario_id INT NOT NULL,
            libro_id INT NOT NULL,
            fecha_prestamo TIMESTAMP DEFAULT NOW(),
            fecha_devolucion_estimada TIMESTAMP DEFAULT NOW(),
            fecha_devolucion_real TIMESTAMP DEFAULT NOW(),
            PRIMARY KEY(id),
            CONSTRAINT fk_usuario FOREIGN KEY (usuario_id) REFERENCES usuarios(id),
            CONSTRAINT fk_libro FOREIGN KEY(libro_id) REFERENCES libros(id)
        );";

        EjecutarConsulta(query);
    }

    static void EjecutarConsulta(string query)
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                Console.WriteLine("Consulta ejecutada correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
    static void MostrarMenuPrincipal()
    {
        CrearTablaUsuarios();
        Console.Clear();
        Console.WriteLine("Menú Principal:");
        Console.WriteLine("1. Menú Usuarios");
        Console.WriteLine("2. Menú Libros");
        Console.WriteLine("3. Menú Préstamos");
        Console.WriteLine("4. Menú Tablas");
        Console.WriteLine("5. Salir");
        Console.Write("Selecciona una opción: ");

        string opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                MostrarMenuUsuarios();
                break;
            case "2":
                MostrarMenuLibros();
                break;
            case "3":
                MostrarMenuPrestamos();
                break;
            case "4":
                MostrarMenuTablas();
                break;
            case "5":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Opción no válida, presiona cualquier tecla para continuar...");
                Console.ReadKey();
                break;
        }
    }
    static void MostrarMenuUsuarios()
    {
        Console.Clear();
        Console.WriteLine("Menú Usuarios:");
        Console.WriteLine("1. Crear Usuario");
        Console.WriteLine("2. Actualizar Usuario");
        Console.WriteLine("3. Borrar Usuario");
        Console.WriteLine("4. Volver al Menú Principal");
        Console.Write("Selecciona una opción: ");

        string opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                CrearUsuario();
                break;
            case "2":
                ActualizarUsuario();
                break;
            case "3":
                BorrarUsuario();
                break;
            case "4":
                return; // Vuelve al menú principal
            default:
                Console.WriteLine("Opción no válida, presiona cualquier tecla para continuar...");
                Console.ReadKey();
                break;
        }
    }
    static void MostrarMenuLibros()
    {
        CrearTablaLibros();
        Console.Clear();
        Console.WriteLine("Menú Libros:");
        Console.WriteLine("1. Agregar Libro");
        Console.WriteLine("2. Actualizar Libro");
        Console.WriteLine("3. Borrar Libro");
        Console.WriteLine("4. Volver al Menú Principal");
        Console.Write("Selecciona una opción: ");

        string opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                AgregarLibro();
                break;
            case "2":
                ActualizarLibro();
                break;
            case "3":
                BorrarLibro();
                break;
            case "4":
                return; // Vuelve al menú principal
            default:
                Console.WriteLine("Opción no válida, presiona cualquier tecla para continuar...");
                Console.ReadKey();
                break;
        }
    }
    static void MostrarMenuPrestamos()
    {
        CrearTablaPrestamos();
        Console.Clear();
        Console.WriteLine("Menú Préstamos:");
        Console.WriteLine("1. Crear Préstamo");
        Console.WriteLine("2. Actualizar Préstamo");
        Console.WriteLine("3. Volver al Menú Principal");
        Console.Write("Selecciona una opción: ");

        string opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                CrearPrestamo();
                break;
            case "2":
                ActualizarPrestamo();
                break;
            case "3":
                return; // Vuelve al menú principal
            default:
                Console.WriteLine("Opción no válida, presiona cualquier tecla para continuar...");
                Console.ReadKey();
                break;
        }
    }
    static void MostrarMenuTablas()
    {
        Console.Clear();
        Console.WriteLine("Menú Tablas:");
        Console.WriteLine("1. Visualizar Tabla de Usuarios");
        Console.WriteLine("2. Visualizar Tabla de Libros");
        Console.WriteLine("3. Visualizar Tabla de Préstamos");
        Console.WriteLine("4. Volver al Menú Principal");
        Console.Write("Selecciona una opción: ");

        string opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                VisualizarTablaUsuarios();
                break;
            case "2":
                VisualizarTablaLibros();
                break;
            case "3":
                VisualizarTablaPrestamos();
                break;
            case "4":
                return; // Vuelve al menú principal
            default:
                Console.WriteLine("Opción no válida, presiona cualquier tecla para continuar...");
                Console.ReadKey();
                break;
        }
    }
    static void CrearUsuario()
    {
        Console.Clear();
        Console.WriteLine("Ingrese los datos del nuevo usuario:");

        Console.Write("Nombre Completo: ");
        string nombreCompleto = Console.ReadLine();

        Console.Write("DNI: ");
        string dni = Console.ReadLine();

        Console.Write("Teléfono: ");
        string telefono = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        string query = "INSERT INTO usuarios (nombre_completo, dni, telefono, email, creado_el, actualizado_el) " +
                       "VALUES (@nombre_completo, @dni, @telefono, @email, NOW(), NOW());";

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@nombre_completo", nombreCompleto);
            command.Parameters.AddWithValue("@dni", dni);
            command.Parameters.AddWithValue("@telefono", telefono);
            command.Parameters.AddWithValue("@email", email);
            command.ExecuteNonQuery();
            Console.WriteLine("Usuario creado exitosamente.");
        }
        Console.ReadKey();
    }

    static void ActualizarUsuario()
    {

        Console.Clear();
        Console.Write("Ingrese el DNI del usuario a actualizar: ");
        string dni = Console.ReadLine();

        // Verificar que el usuario exista
        string query = "SELECT id, nombre_completo, telefono, email, estado FROM usuarios WHERE dni = @dni AND estado = 1;";
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@dni", dni);
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                Console.Clear();
                Console.WriteLine($"Usuario encontrado: {reader["nombre_completo"]}, {reader["telefono"]}, {reader["email"]}");

                Console.Write("Nuevo Teléfono (dejar vacío para no cambiar): ");
                string telefono = Console.ReadLine();
                if (string.IsNullOrEmpty(telefono)) telefono = reader["telefono"].ToString();

                Console.Write("Nuevo Email (dejar vacío para no cambiar): ");
                string email = Console.ReadLine();
                if (string.IsNullOrEmpty(email)) email = reader["email"].ToString();

                string updateQuery = "UPDATE usuarios SET telefono = @telefono, email = @email, actualizado_el = NOW() WHERE dni = @dni;";
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@telefono", telefono);
                updateCommand.Parameters.AddWithValue("@email", email);
                updateCommand.Parameters.AddWithValue("@dni", dni);
                updateCommand.ExecuteNonQuery();

                Console.WriteLine("Usuario actualizado exitosamente.");
            }
            else
            {
                Console.WriteLine("No se encontró un usuario activo con ese DNI.");
            }
        }
        Console.ReadKey();
    }

    static void BorrarUsuario()
    {
        Console.Clear();
        Console.Write("Ingrese el DNI del usuario a borrar: ");
        string dni = Console.ReadLine();

        string query = "UPDATE usuarios SET estado = 0 WHERE dni = @dni AND estado = 1;";
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@dni", dni);
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
                Console.WriteLine("Usuario desactivado exitosamente.");
            else
                Console.WriteLine("No se encontró un usuario activo con ese DNI.");
        }
        Console.ReadKey();
    }

    static void AgregarLibro()
    {
        Console.Clear();
        Console.WriteLine("¿Desea agregar el género del libro?");
        Console.Write("S/N:");
        string opcion = Console.ReadLine();
        if (opcion == "S")
        {
            CrearTablaGenerosLibro();
            VisualizarTablaGeneros();
            AgregarGenero();
        }

        Console.WriteLine("Ingrese los datos del nuevo libro:");

        Console.Write("Nombre del Libro: ");
        string nombreLibro = Console.ReadLine();

        Console.Write("Autor: ");
        string autor = Console.ReadLine();

        Console.Write("Fecha de Lanzamiento (yyyy-mm-dd): ");
        string fechaLanzamientoStr = Console.ReadLine();
        DateTime fechaLanzamiento = DateTime.ParseExact(fechaLanzamientoStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        Console.Write("Género (ID): ");
        int idGenero = int.Parse(Console.ReadLine());

        string query = "INSERT INTO libros (nombre_libro, autor, fecha_lanzamiento, id_genero, creado_el, actualizado_el) " +
                       "VALUES (@nombre_libro, @autor, @fecha_lanzamiento, @id_genero, NOW(), NOW());";

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@nombre_libro", nombreLibro);
            command.Parameters.AddWithValue("@autor", autor);
            command.Parameters.AddWithValue("@fecha_lanzamiento", fechaLanzamiento);
            command.Parameters.AddWithValue("@id_genero", idGenero);
            command.ExecuteNonQuery();
            Console.WriteLine("Libro agregado exitosamente.");
        }
        Console.ReadKey();
    }

    static void ActualizarLibro()
    {
        Console.Clear();
        Console.Write("Ingrese el ID del libro a actualizar: ");
        int idLibro = int.Parse(Console.ReadLine());

        // Verificar que el libro exista
        string query = "SELECT id, nombre_libro, autor, fecha_lanzamiento, estado FROM libros WHERE id = @idLibro AND estado = 1;";
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@idLibro", idLibro);
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                Console.Clear();
                Console.WriteLine($"Libro encontrado: {reader["nombre_libro"]}, {reader["autor"]}, {reader["fecha_lanzamiento"]}");

                // Declarar las variables para los nuevos valores
                string autor = reader["autor"].ToString();
                string nombre_libro = reader["nombre_libro"].ToString();
                string fecha_lanzamiento = Convert.ToDateTime(reader["fecha_lanzamiento"]).ToString("yyyy-MM-dd");

                // Actualización de los campos
                Console.Write("Nuevo Autor (dejar vacío para no cambiar): ");
                string nuevoAutor = Console.ReadLine();
                if (!string.IsNullOrEmpty(nuevoAutor)) autor = nuevoAutor;

                Console.Write("Nuevo Nombre (dejar vacío para no cambiar): ");
                string nuevoNombre = Console.ReadLine();
                if (!string.IsNullOrEmpty(nuevoNombre)) nombre_libro = nuevoNombre;

                Console.Write("Nueva Fecha (dejar vacío para no cambiar): ");
                string nuevaFecha = Console.ReadLine();
                if (!string.IsNullOrEmpty(nuevaFecha)) fecha_lanzamiento = nuevaFecha;

                // Actualización en la base de datos
                string updateQuery = "UPDATE libros SET autor = @autor, nombre_libro = @nombre_libro, fecha_lanzamiento = @fecha_lanzamiento, actualizado_el = NOW() WHERE id = @idLibro;";
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@autor", autor);
                updateCommand.Parameters.AddWithValue("@nombre_libro", nombre_libro);
                updateCommand.Parameters.AddWithValue("@fecha_lanzamiento", fecha_lanzamiento);
                updateCommand.Parameters.AddWithValue("@idLibro", idLibro);
                updateCommand.ExecuteNonQuery();

                Console.WriteLine("Libro actualizado exitosamente.");
            }
            else
            {
                Console.WriteLine("No se encontró un libro activo con ese ID.");
            }
        }
        Console.ReadKey();
    }


    static void BorrarLibro()
    {
        Console.Clear();
        Console.Write("Ingrese el ID del libro a borrar: ");
        int idLibro = int.Parse(Console.ReadLine());

        string query = "UPDATE libros SET estado = 0 WHERE id = @idLibro AND estado = 1;";
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@idLibro", idLibro);
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
                Console.WriteLine("Libro desactivado exitosamente.");
            else
                Console.WriteLine("No se encontró un libro activo con ese ID.");
        }
        Console.ReadKey();
    }

    static void CrearPrestamo()
    {
        Console.Clear();
        Console.Write("Ingrese el DNI del usuario: ");
        string dniUsuario = Console.ReadLine();

        Console.Write("Ingrese el ID del libro: ");
        int idLibro = int.Parse(Console.ReadLine());

        // Verificar si el usuario y el libro están activos
        string queryUsuario = "SELECT estado FROM usuarios WHERE dni = @dniUsuario;";
        string queryLibro = "SELECT estado FROM libros WHERE id = @idLibro;";

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            MySqlCommand cmdUsuario = new MySqlCommand(queryUsuario, connection);
            cmdUsuario.Parameters.AddWithValue("@dniUsuario", dniUsuario);
            var usuarioReader = cmdUsuario.ExecuteReader();
            if (usuarioReader.Read() && (int)usuarioReader["estado"] == 1)
            {
                usuarioReader.Close();
                MySqlCommand cmdLibro = new MySqlCommand(queryLibro, connection);
                cmdLibro.Parameters.AddWithValue("@idLibro", idLibro);
                var libroReader = cmdLibro.ExecuteReader();

                if (libroReader.Read() && (int)libroReader["estado"] == 1)
                {
                    libroReader.Close();
                    DateTime fechaPrestamo = DateTime.Now;
                    DateTime fechaDevolucion = fechaPrestamo.AddDays(7);

                    string insertPrestamo = "INSERT INTO prestamos (usuario_id, libro_id, fecha_prestamo, fecha_devolucion_estimada) " +
                                            "VALUES ((SELECT id FROM usuarios WHERE dni = @dniUsuario), @idLibro, NOW(), @fechaDevolucion);";
                    MySqlCommand cmdPrestamo = new MySqlCommand(insertPrestamo, connection);
                    cmdPrestamo.Parameters.AddWithValue("@dniUsuario", dniUsuario);
                    cmdPrestamo.Parameters.AddWithValue("@idLibro", idLibro);
                    cmdPrestamo.Parameters.AddWithValue("@fechaDevolucion", fechaDevolucion);
                    cmdPrestamo.ExecuteNonQuery();
                    Console.WriteLine("Préstamo realizado exitosamente.");
                }
                else
                {
                    Console.WriteLine("El libro no está disponible.");
                }
            }
            else
            {
                Console.WriteLine("El usuario no está activo.");
            }
        }
        Console.ReadKey();
    }

    static void ActualizarPrestamo()
    {
        Console.Clear();
        Console.Write("Ingrese el ID del préstamo a actualizar: ");
        int idPrestamo = int.Parse(Console.ReadLine());

        string query = "SELECT usuario_id, libro_id, fecha_devolucion_real FROM prestamos WHERE id = @idPrestamo;";
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@idPrestamo", idPrestamo);
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                Console.Clear();
                Console.WriteLine($"Préstamo encontrado: ID Usuario {reader["usuario_id"]}, ID Libro {reader["libro_id"]}");

                Console.Write("Fecha de devolución real (yyyy-mm-dd): ");
                string fechaDevolucionStr = Console.ReadLine();
                DateTime fechaDevolucionReal = DateTime.ParseExact(fechaDevolucionStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                string updateQuery = "UPDATE prestamos SET fecha_devolucion_real = @fechaDevolucionReal WHERE id = @idPrestamo;";
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@fechaDevolucionReal", fechaDevolucionReal);
                updateCommand.Parameters.AddWithValue("@idPrestamo", idPrestamo);
                updateCommand.ExecuteNonQuery();

                // Verificar si el usuario devolvió fuera de término
                if (fechaDevolucionReal > DateTime.Now)
                {
                    string updateUserQuery = "UPDATE usuarios SET estado = 0 WHERE id = @usuarioId;";
                    MySqlCommand updateUserCommand = new MySqlCommand(updateUserQuery, connection);
                    updateUserCommand.Parameters.AddWithValue("@usuarioId", reader["usuario_id"]);
                    updateUserCommand.ExecuteNonQuery();
                    Console.WriteLine("El usuario ha sido desactivado por devolver fuera de término.");
                }

                Console.WriteLine("Préstamo actualizado exitosamente.");
            }
            else
            {
                Console.WriteLine("No se encontró el préstamo.");
            }
        }
        Console.ReadKey();

    }

    static void VisualizarTablaUsuarios()
    {
        Console.Clear();
        Console.WriteLine("Tabla de Usuarios:");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("{0,-5} {1,-30} {2,-10} {3,-15} {4,-30} {5,-20} {6,-10}",
            "ID", "Nombre Completo", "DNI", "Teléfono", "Email", "Creado El", "Estado");
        Console.WriteLine("-----------------------------------------------");

        string query = "SELECT id, nombre_completo, dni, telefono, email, creado_el, estado FROM usuarios ORDER BY id;";

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("{0,-5} {1,-30} {2,-10} {3,-15} {4,-30} {5,-20} {6,-10}",
                    reader["id"],
                    reader["nombre_completo"],
                    reader["dni"],
                    reader["telefono"],
                    reader["email"],
                    Convert.ToDateTime(reader["creado_el"]).ToString("yyyy-MM-dd HH:mm:ss"),
                    reader["estado"] == "1" ? "Inactivo" : "Activo");
            }
        }

        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("Presiona cualquier tecla para volver al menú de tablas...");
        Console.ReadKey();
    }





    static void VisualizarTablaLibros()
    {
        Console.Clear();
        Console.WriteLine("Tabla de Libros:");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("{0,-5} {1,-40} {2,-25} {3,-15} {4,-10} {5,-20} {6,-10}",
            "ID", "Nombre del Libro", "Autor", "Fecha Lanzamiento", "Género", "Creado El", "Estado");
        Console.WriteLine("-----------------------------------------------");

        string query = "SELECT l.id, l.nombre_libro, l.autor, l.fecha_lanzamiento, g.genero, l.creado_el, l.estado " +
                       "FROM libros l INNER JOIN generos_libro g ON l.id_genero = g.id ORDER BY l.id;";

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("{0,-5} {1,-40} {2,-25} {3,-15} {4,-10} {5,-20} {6,-10}",
                    reader["id"],
                    reader["nombre_libro"],
                    reader["autor"],
                    Convert.ToDateTime(reader["fecha_lanzamiento"]).ToString("yyyy-MM-dd"),
                    reader["genero"],
                    Convert.ToDateTime(reader["creado_el"]).ToString("yyyy-MM-dd HH:mm:ss"),
                    reader["estado"] == "1" ? "Activo" : "Inactivo");
            }
        }

        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("Presiona cualquier tecla para volver al menú de tablas...");
        Console.ReadKey();
    }

    static void VisualizarTablaPrestamos()
    {
        Console.Clear();
        Console.WriteLine("Tabla de Préstamos:");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("{0,-5} {1,-10} {2,-10} {3,-20} {4,-20} {5,-20} {6,-20}",
            "ID", "Usuario ID", "Libro ID", "Fecha Préstamo", "Fecha Estimada", "Fecha Real", "Estado");
        Console.WriteLine("-----------------------------------------------");

        string query = "SELECT p.id, p.usuario_id, p.libro_id, p.fecha_prestamo, p.fecha_devolucion_estimada, p.fecha_devolucion_real, p.estado " +
                       "FROM prestamos p ORDER BY p.id;";

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("{0,-5} {1,-10} {2,-10} {3,-20} {4,-20} {5,-20} {6,-10}",
                    reader["id"],
                    reader["usuario_id"],
                    reader["libro_id"],
                    Convert.ToDateTime(reader["fecha_prestamo"]).ToString("yyyy-MM-dd HH:mm:ss"),
                    Convert.ToDateTime(reader["fecha_devolucion_estimada"]).ToString("yyyy-MM-dd HH:mm:ss"),
                    reader["fecha_devolucion_real"] != DBNull.Value ? Convert.ToDateTime(reader["fecha_devolucion_real"]).ToString("yyyy-MM-dd HH:mm:ss") : "No Devuelto",
                    reader["estado"] == "1" ? "Activo" : "Inactivo");
            }
        }

        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("Presiona cualquier tecla para volver al menú de tablas...");
        Console.ReadKey();
    }
    static void AgregarGenero()
    {
        Console.WriteLine("Agregar Nuevo Género de Libro");
        Console.Write("Ingrese el nombre del género (máximo 20 caracteres): ");
        string genero = Console.ReadLine();
        string queryInsertarGenero = "INSERT INTO generos_libro (genero) VALUES (@genero);";
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            MySqlCommand cmdInsertar = new MySqlCommand(queryInsertarGenero, connection);
            cmdInsertar.Parameters.AddWithValue("@genero", genero);
            cmdInsertar.ExecuteNonQuery();

            Console.WriteLine("Género agregado exitosamente.");
        }

        Console.ReadKey();
    }

    static void VisualizarTablaGeneros()
    {
        Console.Clear();
        Console.WriteLine("Tabla de Géneros de Libro:");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("{0,-5} {1,-20}", "ID", "Género");
        Console.WriteLine("-----------------------------------------------");

        string query = "SELECT id, genero FROM generos_libro ORDER BY id;";

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("{0,-5} {1,-20}",
                    reader["id"],
                    reader["genero"]);
            }
        }

        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("Presiona cualquier tecla para volver al menú de tablas...");
        Console.ReadKey();
    }

}





