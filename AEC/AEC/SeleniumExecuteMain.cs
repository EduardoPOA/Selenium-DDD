using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.ComponentModel;
using SimpleInjector;
using Container = SimpleInjector.Container;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AEC;


//classe principal que irá executar
class SeleniumExecuteMain
{
    static void Main(string[] args)
    {

        //nesta linha estou passando os resultados AluraSearchService para dentro da interface do CurstoDTO
        AluraSearchService searchService = new AluraSearchService();

        using (IWebDriver driver = new ChromeDriver())
        {
            try
            {
                string searchCourseWithName = "Formação Modelagem e Melhorias de Processos de Negócios";
                CursoDTO curso = searchService.Search(driver, searchCourseWithName);

                using (var context = new CursoContext())
                {
                    context.Cursos.Add(curso);
                    context.SaveChanges();
                }
                // vou verificar no console do VS2022 se o curso foi inserido no banco de dados
                //PS: conforme lá em cima descrito as informações injetadas estão no memory Database VS2022
                using (var context = new CursoContext())
                {
                    var cursoInserido = context.Cursos.FirstOrDefault(c => c.Titulo == searchCourseWithName);

                    if (cursoInserido != null)
                    {
                        Console.WriteLine("CURSO INSERIDO COM SUCESSO NO DATABASE MEMORY VS!\n");
                        Console.WriteLine($"ID do Curso: {cursoInserido.CursoId}\n");
                        Console.WriteLine($"Título: {cursoInserido.Titulo}\n");
                        Console.WriteLine($"Instrutor: {cursoInserido.Professor} \n");
                        Console.WriteLine($"Carga horária: {cursoInserido.CargaHoraria} \n");
                        Console.WriteLine($"Descrição: {cursoInserido.Descricao}\n");
                    }
                    else
                    {
                        Console.WriteLine("Falha ao inserir o curso no banco de dados.");
                    }
                }
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}


