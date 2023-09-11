using Microsoft.AspNetCore.Mvc;
using TodoList1.Models;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace TodoListMVC.Controllers
{
    public class MantenedorController : Controller
    {
        public async Task<ActionResult> List()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://todolistapi20230910224408.azurewebsites.net/api/tasks/list");
            List<TaskItem> taskList = JsonConvert.DeserializeObject<List<TaskItem>>(json);
            return View(taskList);
        }


        public async Task<IActionResult> CreateTask()
        {
            return View();
        }

            [HttpPost]
        public async Task<IActionResult> CreateTask(TaskItem task)
        {
            if (!ModelState.IsValid)
            {
                // La validación del modelo ha fallado, retorna una vista con errores
                return View(task);
            }

            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(task);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("https://todolistapi20230910224408.azurewebsites.net/api/tasks/add", content);

                if (response.IsSuccessStatusCode)
                {
                    // La tarea se ha creado exitosamente en la API
                    return RedirectToAction("List");
                }
                else
                {
                    // Hubo un error al crear la tarea en la API, maneja el error apropiadamente
                    ModelState.AddModelError(string.Empty, "Error al crear la tarea en la API.");
                    return View(task);
                }
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync($"https://todolistapi20230910224408.azurewebsites.net/api/tasks/find/{id}");
            TaskItem task = JsonConvert.DeserializeObject<TaskItem>(json);
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskItem task)
        {
            if (!ModelState.IsValid)
            {
                // La validación del modelo ha fallado, retorna la vista de edición con errores
                return View(task);
            }

            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(task);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync($"https://todolistapi20230910224408.azurewebsites.net/api/tasks/edit/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    // La tarea se ha editado exitosamente en la API
                    return RedirectToAction("List");
                }
                else
                {
                    // Hubo un error al editar la tarea en la API, maneja el error apropiadamente
                    ModelState.AddModelError(string.Empty, "Error al editar la tarea en la API.");
                    return View(task);
                }
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync($"https://todolistapi20230910224408.azurewebsites.net/api/tasks/find/{id}");
            TaskItem task = JsonConvert.DeserializeObject<TaskItem>(json);
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskItem task)
        {
            using (var httpClient = new HttpClient())
            {

                var response = await httpClient.DeleteAsync($"https://todolistapi20230910224408.azurewebsites.net/api/tasks/delete/{task.Id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al editar la tarea en la API.");
                    return View(task);
                }
            }
        }


    }
}
