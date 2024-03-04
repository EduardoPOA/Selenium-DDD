using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC
{
    // Definição da interface do DDD do serviço de pesquisa
    public interface ISearchService
    {
        CursoDTO Search(IWebDriver driver, string searchCourse);
    }


    // Implementação do código do serviço que ira fazer no site Alura utilizando o serviço
    public class AluraSearchService : ISearchService
    {
        //declarando o pageobjects
        private By textBoxSearch = By.Id("header-barraBusca-form-campoBusca");
        private By linkTitle = By.ClassName("busca-resultado-nome");
        private By innerTextHour = By.XPath("/html/body/section[1]/div/div[2]/div[1]/div/div[1]/div/p[1]");
        private By innerTextDescription = By.ClassName("course-list");
        private By innerTextTeacher = By.ClassName("instructor-title--name");

        public CursoDTO Search(IWebDriver driver, string searchCourse)
        {
            //usei o dotnethelpers para waitelements no selenium 4 nao existe mais
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl("https://www.alura.com.br/");
            driver.Manage().Window.Maximize();

            driver.FindElement(textBoxSearch).SendKeys(searchCourse);
            driver.FindElement(textBoxSearch).Submit();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(linkTitle));
            var linkTitleList = driver.FindElements(linkTitle);
            IWebElement firstTitle = linkTitleList.FirstOrDefault();
            firstTitle.Click();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(innerTextHour));
            var linkTeacherList = driver.FindElements(innerTextTeacher);
            IWebElement firstTeacher = linkTeacherList.FirstOrDefault();
            string teacher = firstTeacher.GetAttribute("innerText").Trim();
            string hour = driver.FindElement(innerTextHour).GetAttribute("innerText").Replace("h", "");
            string description = driver.FindElement(innerTextDescription).GetAttribute("innerText").Trim();

            //inserindo valores nas varíáveis DTO
            CursoDTO curso = new CursoDTO
            {
                Titulo = searchCourse,
                Professor = teacher,
                CargaHoraria = hour,
                Descricao = description
            };

            return curso;
        }
    }
}
