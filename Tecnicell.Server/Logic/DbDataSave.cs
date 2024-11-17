using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using Tecnicell.Server.Context;
using Tecnicell.Server.Models.Results;

namespace Tecnicell.Server.Logic
{
    public class DbDataSave
    {
        public static void DbSave(TecnicellContext context)
        {
            // Realiza la consulta para obtener los datos de la vista
            // Primero, obtén todos los datos de AccessoryViews y AccessoryTypes
            var accessoryTypes = context.AccessoryTypes.ToList();
            var users = context.UserInfos.ToList();
            var currencies = context.Currencies.ToList();
            var sales = context.Sales.ToList();
            var branches = context.Branches.ToList();

            var accessoryViews = context.AccessoryViews.ToList();
            var accessoryHistories = context.AccessoryHistories.Include(model => model.SaleCodeNavigation).ToList();

            var batteriesViews = context.BatteryViews.ToList();
            var batteryHistories = context.BatteryHistories.ToList();
            var screenViews = context.ScreenViews.ToList();
            var screenHistories = context.ScreenHistories.ToList();

            var phoneViews = context.PhoneViews.ToList();
            var phoneHistories = context.PhoneHistories.ToList();
            var phoneRepairViews = context.PhoneRepairViews.ToList();
            var phoneRepairHistories = context.PhoneRepairHistories.ToList();

            // Luego, usa LINQ to Objects para unir y transformar los datos
            var dataAccessoryViews = accessoryViews.Select(model => {
                var accessoryType = accessoryTypes.FirstOrDefault(type => type.AccessoryTypeCode == model.TypeCode);
                return new AccessoryViewResult
                {
                    Código = model.Code,
                    Cantidad = model.Quantity,
                    Estado = model.Available,
                    Nombre = model.Name,
                    Price = model.SalePrice,
                    Tipo_de_Accesorio = accessoryType?.Name // Usa el operador null-conditional para evitar errores si accessoryType es null
                };
            }).ToList();
            var dataAccessoryHistories = accessoryHistories.Select(model
                =>
            {
                var acc = accessoryViews.FirstOrDefault(acc => acc.Code == model.AccessoryCode);
                var userInfo = users.FirstOrDefault(user => user.UserCode == model.UserCode);
                var branch = branches.FirstOrDefault(branch => branch.BranchCode == model.ToBranch);
                var sale = sales.FirstOrDefault(sale => sale.SaleCode == model.SaleCode);
                var currency = currencies.FirstOrDefault(currency => currency.CurrencyCode == sale?.CurrencyCode);
                return new AccessoryHistoryResult
                {
                    Codigo = model.AccessoryCode,
                    Accion = model.ActionHistory,
                    Descripción = model.Description,
                    Fecha = model.Date,
                    Garantía = sale?.Warranty,
                    Moneda = currency?.CurrencyName,
                    Usuario = userInfo?.Name,
                    Nombre = acc?.Name,
                    Precio = sale?.Cost,
                    Sucursal = branch?.Name
                };
            }).ToList();
            var dataBatteryViews = batteriesViews.Select(model => {
                return new BatteryViewResult
                {
                    Código = model.Code,
                    Cantidad = model.Quantity,
                    Estado = model.Available,
                    Nombre = model.Name,
                    Precio = model.SalePrice,
                    Garantía = model.Warranty,
                    Marca = model.Type 
                };
            }).ToList();
            var dataBatteryHistories = batteryHistories.Select(model
                =>
            {
                var bat = batteriesViews.FirstOrDefault(acc => acc.Code == model.BatteryCode);
                var userInfo = users.FirstOrDefault(user => user.UserCode == model.UserCode);
                var branch = branches.FirstOrDefault(branch => branch.BranchCode == model.ToBranch);
                var sale = sales.FirstOrDefault(sale => sale.SaleCode == model.SaleCode);
                var currency = currencies.FirstOrDefault(currency => currency.CurrencyCode == sale?.CurrencyCode);
                return new BatteryHistoryResult
                {
                    Codigo = model.BatteryCode,
                    Accion = model.ActionHistory,
                    Descripción = model.Description,
                    Fecha = model.Date,
                    Garantía = sale?.Warranty,
                    Moneda = currency?.CurrencyName,
                    Usuario = userInfo?.Name,
                    Nombre = bat?.Type + " " + bat?.Name,
                    Precio = sale?.Cost,
                    Sucursal = branch?.Name
                };
            }).ToList();
            var dataScreenViews = screenViews.Select(model => {
                return new ScreenViewResult
                {
                    Código = model.Code,
                    Cantidad = model.Quantity,
                    Estado = model.Available,
                    Nombre = model.Name,
                    Precio = model.SalePrice,
                    Garantía = model.Warranty,
                    Marca = model.Type
                };
            }).ToList();

            var dataScreenHistories = screenHistories.Select(model
                =>
            {
                var scr = batteriesViews.FirstOrDefault(acc => acc.Code == model.ScreenCode);
                var userInfo = users.FirstOrDefault(user => user.UserCode == model.UserCode);
                var branch = branches.FirstOrDefault(branch => branch.BranchCode == model.ToBranch);
                var sale = sales.FirstOrDefault(sale => sale.SaleCode == model.SaleCode);
                var currency = currencies.FirstOrDefault(currency => currency.CurrencyCode == sale?.CurrencyCode);
                return new ScreenHistoryResult
                {
                    Codigo = model.ScreenCode,
                    Accion = model.ActionHistory,
                    Descripción = model.Description,
                    Fecha = model.Date,
                    Garantía = sale?.Warranty,
                    Moneda = currency?.CurrencyName,
                    Usuario = userInfo?.Name,
                    Nombre = scr?.Type + " " + scr?.Name,
                    Precio = sale?.Cost,
                    Sucursal = branch?.Name
                };
            }).ToList();
            var dataPhoneViews = phoneViews.Select(model => {
                return new PhoneViewResult
                {
                    Imei = model.Code,
                    Nombre = model.Name,
                    Precio = model.SalePrice,
                    Marca = model.Type,
                    Descripción = model.ActionDescription,
                    Estado = model.CurrentState
                };
            }).ToList();
            var dataPhoneHistories = phoneHistories.Select(model
                =>
            {
                var phone = phoneViews.FirstOrDefault(acc => acc.Code == model.Imei);
                var userInfo = users.FirstOrDefault(user => user.UserCode == model.UserCode);
                var branch = branches.FirstOrDefault(branch => branch.BranchCode == model.ToBranch);
                var sale = sales.FirstOrDefault(sale => sale.SaleCode == model.SaleCode);
                var currency = currencies.FirstOrDefault(currency => currency.CurrencyCode == sale?.CurrencyCode);
                return new PhoneHistoryResult
                {
                    Imei = model.Imei,
                    Accion = model.ActionHistory,
                    Descripción = model.Description,
                    Fecha = model.Date,
                    Garantía = sale?.Warranty,
                    Moneda = currency?.CurrencyName,
                    Usuario = userInfo?.Name,
                    Nombre = phone?.Type + " " + phone?.Name,
                    Precio = sale?.Cost,
                    Sucursal = branch?.Name
                };
            }).ToList();
            var dataPhoneRepairViews = phoneRepairViews.Select(model => {
                return new PhoneRepairViewResult
                {
                    Imei = model.Code,
                    Nombre = model.Name,
                    Precio = model.Price,
                    Marca = model.Type,
                    Descripción = model.ActionDescription,
                    Cliente_CI = model.CustomerId,
                    Cliente_Nombre = model.CustomerName,
                    Cliente_Teléfono = model.CustomerNumber,
                    Fecha_Ultima_Accion = model.Date,
                    Estado = model.CurrentState
                };
            }).ToList();
            var dataPhoneRepairHistories = phoneRepairHistories.Select(model
                =>
            {
                var phone = phoneViews.FirstOrDefault(acc => acc.Code == model.Imei);
                var userInfo = users.FirstOrDefault(user => user.UserCode == model.UserCode);
                var branch = branches.FirstOrDefault(branch => branch.BranchCode == model.ToBranch);
                var sale = sales.FirstOrDefault(sale => sale.SaleCode == model.SaleCode);
                var currency = currencies.FirstOrDefault(currency => currency.CurrencyCode == sale?.CurrencyCode);
                return new PhoneRepairHistoryResult
                {
                    Imei = model.Imei,
                    Accion = model.ActionHistory,
                    Descripción = model.Description,
                    Fecha = model.Date,
                    Garantía = sale?.Warranty,
                    Moneda = currency?.CurrencyName,
                    Usuario = userInfo?.Name,
                    Nombre = phone?.Type + " " + phone?.Name,
                    Precio = sale?.Cost,
                    Sucursal = branch?.Name
                };
            }).ToList();

            // Ruta donde se guardará el archivo CSV
            string filePath = $"C://DbData/{DateTime.Now:dd-MM-yyyy_HH-mm-ss}/";

            // Asegúrate de que el directorio exista
            ExportToCsv(dataAccessoryViews, filePath, "Accesorios");
            ExportToCsv(dataAccessoryHistories, filePath, "Accesorios Historiales");
            ExportToCsv(dataBatteryViews, filePath, "Baterias");
            ExportToCsv(dataBatteryHistories, filePath, "Baterias Historiales");
            ExportToCsv(dataScreenViews, filePath, "Pantallas");
            ExportToCsv(dataScreenHistories, filePath, "Pantallas Historiales");
            ExportToCsv(dataPhoneViews, filePath, "Telefonos");
            ExportToCsv(dataPhoneHistories, filePath, "Telefonos Historiales");
            ExportToCsv(dataPhoneRepairViews, filePath, "Trabjos Diarios");
            ExportToCsv(dataPhoneRepairHistories, filePath, "Trabjos Diarios Historiales");

            string sourcePath = "DbData/tecnicell.db";
            string backupPath = $"{filePath}backup_{DateTime.Now:yyyyMMdd_HHmmss}.db";

            File.Copy(sourcePath, backupPath);
            Console.WriteLine($"Copia de seguridad creada en: {backupPath}");
        }
        private static void ExportToCsv<T>(List<T> data, string filePath, string fileName) {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (var writer = new StreamWriter(filePath + fileName + ".csv")) 
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture) { 
                Delimiter = ",", 
                Encoding = Encoding.UTF8,
                HasHeaderRecord = true 
            })) 
            { 
                csv.WriteRecords(data); 
            } 
            Console.WriteLine($"Datos exportados a {filePath}"); 
        }
    }
}