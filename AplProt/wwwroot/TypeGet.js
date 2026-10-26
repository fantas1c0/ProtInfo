document.addEventListener('DOMContentLoaded', async () => {
    const selectElement = document.getElementById('type');

    try {
        const response = await fetch('/api/list_labs');
        if (!response.ok) throw new Error('Ошибка данных');

        const data = await response.json();

        // Очищаем select
        selectElement.innerHTML = '<option value="">Выберите вариант...</option>';

        // Заполняем новыми option
        Object.entries(data).forEach(([id, name]) => {
            const option = document.createElement('option');
            option.value = id;
            option.textContent = name;
            selectElement.appendChild(option);
        });
    } catch (error) {
        console.error(error);
        selectElement.innerHTML = '<option value="">Ошибка загрузки</option>';
    }
});