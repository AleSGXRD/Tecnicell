using Microsoft.EntityFrameworkCore;
using System.Xml;
using Tecnicell.Server.Context;
using Tecnicell.Server.Controllers;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Tools;

namespace Tecnicell.Server.Logic
{
    public class DbInitializer
    {
        public static void Initialize(TecnicellContext context)
        {
            context.Database.EnsureCreated();

            // Comprueba si la base de datos ya contiene datos
            if (context.UserAccounts.Any())
            {
                return;   // La base de datos ya ha sido inicializada
            }

            string sqlQueriesViews = @"
                DROP VIEW IF EXISTS Accessory_View;
                DROP VIEW IF EXISTS Battery_View;
                DROP VIEW IF EXISTS Phone_Repair_View;
                DROP VIEW IF EXISTS Phone_View;
                DROP VIEW IF EXISTS Screen_View;

                CREATE VIEW Accessory_View AS
                  SELECT 
                    ac.accessory_code AS code,
                    t.name AS type,
                    t.accessory_type_code AS type_Code,
                    ac.name,
                    ac.sale_price,
                    ac.image_code,
                    sum(CASE
                      WHEN h.action_history = 'Entrada' THEN h.quantity
                      ELSE 0
                    END) + sum(CASE
                      WHEN h.action_history = 'Transferido desde otra sucursal' THEN h.quantity
                      ELSE 0
                    END) - sum(CASE
                      WHEN h.action_history = 'Salida' THEN h.quantity
                      ELSE 0
                    END) - sum(CASE
                      WHEN h.action_history = 'Transferido hacia otra sucursal' THEN h.quantity
                      ELSE 0
                    END) AS quantity,
                    CASE
                      WHEN (sum(CASE
                        WHEN h.action_history = 'Entrada' THEN h.quantity
                        ELSE 0
                      END) + sum(CASE
                        WHEN h.action_history = 'Transferido desde otra sucursal' THEN h.quantity
                        ELSE 0
                      END) - sum(CASE
                        WHEN h.action_history = 'Salida' THEN h.quantity
                        ELSE 0
                      END) - sum(CASE
                        WHEN h.action_history = 'Transferido hacia otra sucursal' THEN h.quantity
                        ELSE 0
                      END)) > 0 THEN 'Disponible'
                      ELSE 'No Disponible'
                    END AS available
                  FROM accessory ac
                  JOIN accessory_type t ON ac.accessory_type = t.accessory_type_code
                  JOIN accessory_history h ON ac.accessory_code = h.accessory_code
                  GROUP BY t.name, t.accessory_type_code, ac.accessory_code;
                CREATE VIEW Battery_View AS
                SELECT 
                  bat.battery_code AS Code,
                  t.name AS Type,
                  bat.name,
                  bat.sale_price,
                  bat.warranty,
                  bat.image_code,
                  SUM(CASE WHEN h.action_history = 'Entrada' THEN h.quantity ELSE 0 END) +
                  SUM(CASE WHEN h.action_history = 'Transferido desde otra sucursal' THEN h.quantity ELSE 0 END) -
                  SUM(CASE WHEN h.action_history = 'Salida' THEN h.quantity ELSE 0 END) -
                  SUM(CASE WHEN h.action_history = 'Transferido hacia otra sucursal' THEN h.quantity ELSE 0 END) AS quantity,
                  CASE
                    WHEN SUM(CASE WHEN h.action_history = 'Entrada' THEN h.quantity ELSE 0 END) +
                         SUM(CASE WHEN h.action_history = 'Transferido desde otra sucursal' THEN h.quantity ELSE 0 END) -
                         SUM(CASE WHEN h.action_history = 'Salida' THEN h.quantity ELSE 0 END) -
                         SUM(CASE WHEN h.action_history = 'Transferido hacia otra sucursal' THEN h.quantity ELSE 0 END)  > 0
                    THEN 'Disponible'
                    ELSE 'No Disponible'
                  END AS available
                FROM battery bat
                JOIN brand t ON bat.brand = t.name
                JOIN battery_history h ON bat.battery_code = h.battery_code
                GROUP BY t.name, bat.battery_code;

                CREATE VIEW Screen_View AS
                SELECT 
                  scr.screen_code AS Code,
                  t.name AS Type,
                  scr.name,
                  scr.sale_price,
                  scr.warranty,
                  scr.image_code,
                  SUM(CASE WHEN h.action_history = 'Entrada' THEN h.quantity ELSE 0 END) +
                  SUM(CASE WHEN h.action_history = 'Transferido desde otra sucursal' THEN h.quantity ELSE 0 END) -
                  SUM(CASE WHEN h.action_history = 'Salida' THEN h.quantity ELSE 0 END) -
                  SUM(CASE WHEN h.action_history = 'Transferido hacia otra sucursal' THEN h.quantity ELSE 0 END) AS quantity,
                  CASE
                    WHEN SUM(CASE WHEN h.action_history = 'Entrada' THEN h.quantity ELSE 0 END) +
                         SUM(CASE WHEN h.action_history = 'Transferido desde otra sucursal' THEN h.quantity ELSE 0 END) -
                         SUM(CASE WHEN h.action_history = 'Salida' THEN h.quantity ELSE 0 END) -
                         SUM(CASE WHEN h.action_history = 'Transferido hacia otra sucursal' THEN h.quantity ELSE 0 END) > 0
                    THEN 'Disponible'
                    ELSE 'No Disponible'
                  END AS available
                FROM screen scr
                JOIN brand t ON scr.brand = t.name
                JOIN screen_history h ON scr.screen_code = h.screen_code
                GROUP BY t.name, scr.screen_code;

                CREATE VIEW Phone_View AS
                SELECT 
                  p.imei AS code,
                  t.name AS type,
                  p.name,
                  p.sale_price,
                  p.image_code,
                  MAX(CASE 
                      WHEN fila_primera = 1 THEN 
                      (SELECT cost FROM sale s WHERE h.sale_code = s.sale_code)
                      END) AS cost,
                  MAX(CASE 
                      WHEN fila_ultima = 1 THEN 
                      CASE
                          WHEN h.action_history = 'Salida' THEN 'No Disponible'
                          WHEN h.action_history = 'Entrada' THEN 'Disponible'
                          WHEN h.action_history = 'Pieza Extraida' THEN 'Por piezas'
                          WHEN h.action_history = 'Pieza Puesta' THEN 'Por piezas'
                          WHEN h.action_history = 'Armado' THEN 'Disponible'
                          WHEN h.action_history = 'Transferido hacia otra sucursal' THEN 'En otra Sucursal'
                          WHEN h.action_history = 'Merma' THEN 'Merma'
                          ELSE 'Estado desconocido'
                      END 
                      END) AS current_state,
                  MAX(CASE 
                      WHEN fila_ultima = 1 THEN h.description
                      END) AS Action_Description
                FROM phone p
                JOIN brand t ON p.brand = t.name
                JOIN (
                    SELECT
                      p.imei,
                      his.action_history,
                      his.date,
                      his.sale_code,
                      his.description,
                      ROW_NUMBER() OVER (PARTITION BY p.imei ORDER BY his.date DESC) AS fila_ultima,
                      ROW_NUMBER() OVER (PARTITION BY p.imei ORDER BY his.date ASC) AS fila_primera
                    FROM phone p
                    JOIN phone_history his ON p.imei = his.imei
                ) h ON p.imei = h.imei
                GROUP BY t.name, p.imei;

                CREATE VIEW Phone_Repair_View AS
                WITH historial_filtrado AS (
                    SELECT 
                        p_1.imei,
                        his.action_history,
                        his.date,
                        his.sale_code,
                        his.description,
                        ROW_NUMBER() OVER (PARTITION BY p_1.imei ORDER BY his.date DESC) AS fila_ultima
                    FROM phone_repair p_1
                    JOIN phone_repair_history his ON p_1.imei = his.imei
                )
                SELECT 
                    p.imei AS code,
                    t.name AS type,
                    p.name,
                    p.customer_name,
                    p.customer_id,
                    p.customer_number,
                    p.price,
                    p.image_code,
                    (SELECT historial_filtrado.date 
                     FROM historial_filtrado 
                     WHERE p.imei = historial_filtrado.imei AND historial_filtrado.fila_ultima = 1) AS date,
                    MAX(
                        CASE 
                            WHEN h.fila_ultima = 1 THEN 
                                CASE
                                    WHEN h.action_history = 'Salida' THEN 'Entregado'
                                    WHEN h.action_history = 'Entrada' THEN 'Reparando'
                                    WHEN h.action_history = 'Reparado' THEN 'Listo'
                                    WHEN h.action_history = 'Pieza Extraida' THEN 'Reparando'
                                    WHEN h.action_history = 'Pieza Puesta' THEN 'Reparando'
                                    WHEN h.action_history = 'Armado' THEN 'Listo'
                                    WHEN h.action_history = 'Transferido hacia otra sucursal' THEN 'Otra Sucursal'
                                    WHEN h.action_history = 'Merma' THEN 'Merma'
                                    ELSE 'Estado desconocido'
                                END
                            ELSE NULL
                        END
                    ) AS current_state,
                    MAX(
                        CASE 
                            WHEN h.fila_ultima = 1 THEN h.description
                            ELSE NULL
                        END
                    ) AS action_description
                FROM phone_repair p
                JOIN brand t ON p.brand = t.name
                JOIN historial_filtrado h ON p.imei = h.imei
                GROUP BY t.name, p.imei
                ORDER BY date;

                ";
            context.Database.ExecuteSqlRaw(sqlQueriesViews);


            string sqlQueries = @"
                INSERT INTO currency(currency_code, currency_name) VALUES ('pN52_BZXv_Due0_4Gma', 'MN');
                INSERT INTO role(
	                    role_code, name)
	                    VALUES ('KKYW_rkaT_Sñ64_jtRK', 'Administrador'),
	                    ('YHYc_ISif_7os0_ZqBR', 'Trabajador'),
	                    ('tY6X_f5Ul_ZFEk_oj8J', 'Auxiliar');
                INSERT INTO action_history(
	                name)
	                VALUES ('Armado'),('Entrada'), ('Salida'), ('Pieza Extraida'), ('Pieza Puesta'),('Merma'),('Transferido hacia otra sucursal'),('Reparado');
            ";
            Branch branch = new Branch
            {
                Name = "Matanzas",
                BranchCode = GeneratorCode.GenerateCode(4)
            };
            context.Branches.Add(branch);

            context.Database.ExecuteSqlRaw(sqlQueries);
            // Aquí agregas las consultas o inserciones que necesitas
            UserInfo userInfo = new UserInfo
            {
                UserCode = GeneratorCode.GenerateCode(4),
                Role = "KKYW_rkaT_Sñ64_jtRK",
                Name = "Admin",
                Branch = branch.BranchCode
            };
            UserAccount userAccount = new UserAccount
            {
                UserCode = userInfo.UserCode,
                Name = "Admin",
                Password = Encrypt.GetSHA256("123123")
            };
            context.UserInfos.Add(userInfo);
            context.UserAccounts.Add(userAccount);

            context.SaveChanges();
        }
    }
}
