using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SistemaContabilidadAltosDelAbejonal.Pruebas
{
    public class Automatizacion
    {
        [TestFixture]
        public class InventarioTests
        {
            private IWebDriver driver;

            [SetUp]
            public void Setup()
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
            }


            [Test]
            public void CrearProducto()
            {
                driver.Navigate().GoToUrl("127.1.0.0:44324/Login/IniciarSesion");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.FindElement(By.Id("Correo")).SendKeys("admin@gmail.com");
                driver.FindElement(By.Id("Contraseña")).SendKeys("123");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.FindElement(By.CssSelector(".btn-primary")).Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.Navigate().GoToUrl("https://localhost:44324/Productos/Stock");
                driver.FindElement(By.Id("moduloInventario")).Click();
                driver.FindElement(By.CssSelector("button.btn.btn-primary")).Click();
                driver.FindElement(By.Id("nombreProducto")).SendKeys("Nuevo Producto");
                driver.FindElement(By.Id("tipoProducto")).SendKeys("Electrónica");
                driver.FindElement(By.Id("cantidadProducto")).SendKeys("10");
                driver.FindElement(By.Id("precioProducto")).SendKeys("100.50");
                driver.FindElement(By.Id("btnGuardarProducto")).Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }

            [TearDown]
            public void Teardown()
            {
                try
                {
                    if (driver != null)
                    {
                        driver.Quit();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error en el TearDown: " + ex.Message);
                }
            }
        }
    }

    [TestFixture]
    public class EditarProductoTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        public void IniciarSesion()
        {
            driver.Navigate().GoToUrl("https://localhost:44324/Login/IniciarSesion");

            driver.FindElement(By.Id("Correo")).SendKeys("fran@gmail.com");
            driver.FindElement(By.Id("Contraseña")).SendKeys("fran123");

            driver.FindElement(By.CssSelector(".btn-primary")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void EditarProducto()
        {
            driver.Navigate().GoToUrl("https://localhost:44324/Productos/Stock");
            driver.FindElement(By.Id("moduloInventario")).Click();
            IWebElement primerBotonEditar = driver.FindElement(By.XPath("//table[@id='datatable']//tbody/tr[1]//button[contains(@class, 'btn-info')]"));
            primerBotonEditar.Click();
            IWebElement campoPrecio = driver.FindElement(By.Id("precioProducto"));
            campoPrecio.Clear(); 
            campoPrecio.SendKeys("150.75");

            IWebElement campoCantidad = driver.FindElement(By.Id("cantidadProducto"));
            campoCantidad.Clear();
            campoCantidad.SendKeys("20");

            driver.FindElement(By.Id("btnGuardarProducto")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }

    [TestFixture]
    public class EliminarProductoTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        public void IniciarSesion()
        {
            driver.Navigate().GoToUrl("https://localhost:44324/Login/IniciarSesion");

            driver.FindElement(By.Id("Correo")).SendKeys("fran@gmail.com");
            driver.FindElement(By.Id("Contraseña")).SendKeys("fran123");

            driver.FindElement(By.CssSelector(".btn-primary")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void EliminarPrimerProducto()
        {
            driver.Navigate().GoToUrl("https://localhost:44324/Productos/Stock");
            driver.FindElement(By.Id("moduloInventario")).Click();

            IWebElement botonEliminar = driver.FindElement(By.XPath("//table[@id='datatable']//tbody/tr[1]//button[contains(@class, 'btn-danger')]"));
            botonEliminar.Click();

            IWebElement botonEliminarConfirmar = driver.FindElement(By.XPath("//div[@id='modal1-']//button[contains(@class, 'btn-danger')]"));
            botonEliminarConfirmar.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}