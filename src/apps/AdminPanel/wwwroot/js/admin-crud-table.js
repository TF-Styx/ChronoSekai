window.App = window.App || {};

window.App.initializeCrudTable = async (options) => {
    const containerId = options.containerId;
    const apiEndpoint = options.apiEndpoint;
    const entityTitle = options.entityTitle;

    const container = document.getElementById(containerId);
    if (!container) {
        console.error(`Контейнер с ID "${containerId}" не найден.`);
        return;
    }

    // --- Модальное окно для Добавления и Редактирования ---
    const modalElement = document.getElementById('addEntityModal');
    const addEntityModal = new bootstrap.Modal(modalElement);
    const modalTitle = document.getElementById('addEntityModalLabel');
    const saveButton = document.getElementById('saveEntityBtn');
    const form = document.getElementById('addEntityForm');
    const entityIdInput = document.getElementById('entityId');
    const entityNameInput = document.getElementById('entityName');
    const modalError = document.getElementById('modalError');

    // --- Модальное окно для Удаления ---
    const deleteModalElement = document.getElementById('deleteConfirmModal');
    const deleteConfirmModal = new bootstrap.Modal(deleteModalElement);
    const deleteConfirmText = document.getElementById('deleteConfirmText');
    const confirmDeleteBtn = document.getElementById('confirmDeleteBtn');
    const deleteModalError = document.getElementById('deleteModalError');

    // --- Переменная для хранения ID удаляемого элемента ---
    let itemIdToDelete = null;

    async function fetchAndRender() {
        container.innerHTML = `<h2>${entityTitle}</h2><p>Загрузка...</p>`;
        try {
            const response = await fetch(apiEndpoint);
            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(`HTTP ошибка! Статус: ${response.status}, сообщение: ${errorText}`);
            }
            const items = await response.json();
            renderTable(items);
            attachEventListeners();
        } catch (error) {
            console.error('Ошибка при загрузке данных:', error);
            container.innerHTML = `
                <h2>${entityTitle}</h2>
                <p class="text-danger">Ошибка при загрузке данных: ${error.message}</p>
                <button class="btn btn-info" onclick="location.reload()">Повторить попытку</button>
            `;
        }
    }

    function renderTable(items) {
        let contentHtml = `<h2>${entityTitle}</h2>`;
        contentHtml += `<button id="addBtn" class="btn btn-primary mb-3">Добавить</button>`;

        if (!items || items.length === 0) {
            contentHtml += `<p>Элементы не найдены.</p>`;
        } else {
            contentHtml += `
                <table class="table table-striped">
                    <thead>
                        <tr><th>ID</th><th>Название</th><th>Действия</th></tr>
                    </thead>
                    <tbody>
            `;
            items.forEach(item => {
                contentHtml += `
                    <tr>
                        <td>${item.id}</td>
                        <td>${item.name}</td>
                        <td>
                            <button class="btn btn-sm btn-warning edit-btn" data-id="${item.id}">Редактировать</button>
                            <button class="btn btn-sm btn-danger delete-btn" data-id="${item.id}">Удалить</button>
                        </td>
                    </tr>
                `;
            });
            contentHtml += `</tbody></table>`;
        }
        container.innerHTML = contentHtml;
    }

    function handleAddClick() {
        form.reset();
        entityIdInput.value = '';
        modalTitle.textContent = `Добавить "${entityTitle}"`;
        modalError.style.display = 'none';
        addEntityModal.show();
    }

    function handleEditClick(e) {
        const button = e.target;
        const itemId = button.dataset.id;
        const itemName = button.closest('tr').querySelector('td:nth-child(2)').textContent;
        form.reset();
        modalError.style.display = 'none';
        modalTitle.textContent = `Редактировать элемент #${itemId}`;
        entityIdInput.value = itemId;
        entityNameInput.value = itemName;
        addEntityModal.show();
    }

    async function handleFormSubmit() {
        const nameValue = entityNameInput.value.trim();
        const entityId = entityIdInput.value;
        if (!nameValue) {
            modalError.textContent = 'Название не может быть пустым.';
            modalError.style.display = 'block';
            return;
        }
        const isEditing = !!entityId;
        const url = isEditing ? `${apiEndpoint}/${entityId}` : apiEndpoint;
        const method = isEditing ? 'PUT' : 'POST';
        const dto = { name: nameValue };
        try {
            const response = await fetch(url, {
                method: method,
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(dto)
            });
            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(errorText || `Ошибка сервера: ${response.status}`);
            }
            addEntityModal.hide();
            await fetchAndRender();
        } catch (error) {
            modalError.textContent = `Ошибка при сохранении: ${error.message}`;
            modalError.style.display = 'block';
            console.error('Ошибка при отправке данных:', error);
        }
    }


    function handleDeleteClick(e) {
        itemIdToDelete = e.target.dataset.id;
        const itemName = e.target.closest('tr').querySelector('td:nth-child(2)').textContent;

        // Устанавливаем текст и показываем окно
        deleteConfirmText.textContent = `Вы уверены, что хотите удалить "${itemName}" (ID: ${itemIdToDelete})?`;
        deleteModalError.style.display = 'none'; // Скрываем старые ошибки
        deleteConfirmModal.show();
    }

    // НОВАЯ ФУНКЦИЯ: Выполняет фактическое удаление после подтверждения
    async function executeDelete() {
        if (!itemIdToDelete) return;

        try {
            const url = `${apiEndpoint}/${itemIdToDelete}`;
            const response = await fetch(url, {
                method: 'DELETE'
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(errorText || `Ошибка сервера: ${response.status}`);
            }

            // Успех
            deleteConfirmModal.hide();
            await fetchAndRender();

        } catch (error) {
            console.error('Ошибка при удалении элемента:', error);
            // Показываем ошибку прямо в модальном окне
            deleteModalError.textContent = `Не удалось удалить элемент: ${error.message}`;
            deleteModalError.style.display = 'block';
        } finally {
            itemIdToDelete = null; // Сбрасываем ID в любом случае
        }
    }

    function attachEventListeners() {
        // Назначаем обработчик на кнопку "Добавить"
        const addButton = document.getElementById('addBtn');
        if (addButton) {
            addButton.addEventListener('click', handleAddClick);
        }

        // Назначаем обработчики на ВСЕ кнопки "Редактировать"
        container.querySelectorAll('.edit-btn').forEach(button => {
            button.addEventListener('click', handleEditClick);
        });

        // Назначаем обработчики на ВСЕ кнопки "Удалить"
        container.querySelectorAll('.delete-btn').forEach(button => {
            button.addEventListener('click', handleDeleteClick);
        });
    }

    if (saveButton) {
        saveButton.addEventListener('click', handleFormSubmit);
    } else {
        console.error('Кнопка сохранения (saveEntityBtn) не найдена!');
    }

    if (confirmDeleteBtn) {
        confirmDeleteBtn.addEventListener('click', executeDelete);
    } else {
        console.error('Кнопка подтверждения удаления (confirmDeleteBtn) не найдена!');
    }

    await fetchAndRender();
};