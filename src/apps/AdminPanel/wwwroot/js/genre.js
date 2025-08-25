document.addEventListener('DOMContentLoaded', async () => {
    const productsContainer = document.getElementById('products-container');

    // Функция для получения и отображения продуктов
    async function fetchAndRenderProducts() {
        productsContainer.innerHTML = '<h2>Список продуктов</h2><p>Загрузка...</p>'; // Показываем индикатор загрузки

        try {
            // Отправляем запрос к нашему "мини-API" ASP.NET Core MVC
            const response = await fetch('/api/admin-genre'); // Путь к ProductsApiController

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(`HTTP error! status: ${response.status}, message: ${errorText}`);
            }

            const products = await response.json(); // Десериализуем JSON в массив объектов

            if (products.length === 0) {
                productsContainer.innerHTML = '<h2>Список продуктов</h2><p>Продуктов не найдено.</p>';
                return;
            }

            // Создаем HTML-таблицу
            let tableHtml = `
                <h2>Список 'статус перевода'</h2>
                <button id="addProductBtn" class="btn btn-primary mb-3">Добавить продукт</button>
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Название</th>
                        </tr>
                    </thead>
                    <tbody>
            `;

            products.forEach(product => {
                tableHtml += `
                    <tr>
                        <td>${product.id}</td>
                        <td>${product.name}</td>
                        <td>
                            <button class="btn btn-sm btn-warning edit-btn" data-id="${product.id}">Редактировать</button>
                            <button class="btn btn-sm btn-danger delete-btn" data-id="${product.id}">Удалить</button>
                        </td>
                    </tr>
                `;
            });

            tableHtml += `
                    </tbody>
                </table>
            `;

            productsContainer.innerHTML = tableHtml;

            // Добавляем обработчики событий для кнопок (пример)
            document.querySelectorAll('.edit-btn').forEach(button => {
                button.addEventListener('click', (e) => {
                    const productId = e.target.dataset.id;
                    alert(`Редактировать продукт с ID: ${productId}`);
                    // Здесь будет логика для открытия модального окна редактирования
                    // с загрузкой данных продукта через fetch('/api/ProductsApi/' + productId)
                });
            });

            document.querySelectorAll('.delete-btn').forEach(button => {
                button.addEventListener('click', async (e) => {
                    const productId = e.target.dataset.id;
                    if (confirm(`Вы уверены, что хотите удалить продукт с ID: ${productId}?`)) {
                        try {
                            const deleteResponse = await fetch(`/api/ProductsApi/${productId}`, {
                                method: 'DELETE'
                            });
                            if (!deleteResponse.ok) {
                                const errorText = await deleteResponse.text();
                                throw new Error(`HTTP error! status: ${deleteResponse.status}, message: ${errorText}`);
                            }
                            alert('Продукт успешно удален!');
                            fetchAndRenderProducts(); // Обновляем список
                        } catch (error) {
                            console.error('Ошибка при удалении продукта:', error);
                            alert('Ошибка при удалении продукта: ' + error.message);
                        }
                    }
                });
            });

            document.getElementById('addProductBtn').addEventListener('click', () => {
                alert('Открытие формы добавления продукта...');
                // Здесь можно открыть модальное окно с формой,
                // отправить POST запрос через fetch('/api/ProductsApi')
                // и затем вызвать fetchAndRenderProducts()
            });

        } catch (error) {
            console.error('Ошибка при загрузке продуктов:', error);
            productsContainer.innerHTML = `
                <h2>Список продуктов</h2>
                <p class="text-danger">Ошибка при загрузке данных: ${error.message}</p>
                <button class="btn btn-info" onclick="window.location.reload()">Повторить попытку</button>
            `;
        }
    }

    // Вызываем функцию при загрузке страницы
    fetchAndRenderProducts();
});