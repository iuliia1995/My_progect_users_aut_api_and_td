async function UpdateUser() {
    const userId = "89333dd4-3715-421f-b4b8-91c99d9ab455";

    const updateData = {
        login: "Updated_User_JS",
        email: "updated_email@gmail.com",
        password: "newpassword123"
    };

    try {
        const API = `http://localhost:5043/api/User/${userId}`;

        const request = await fetch(API, {
            method: "PUT",
            headers: { "Content-Type": "application/json" }, 
            body: JSON.stringify(updateData)
        });

        if (!request.ok) {
            console.error(`Ошибка при обновлении. Статус ${request.status}`);
            const errorText = await request.text();
            console.error("Детали ", errorText);
            return; 
        }

        console.log("Пользователь успешно обновлен!");

    } catch (error) {
        console.log("Ошибка сети:", error);
    }
}

async function DeleteUserByID() {
    const userId = "89333dd4-3715-421f-b4b8-91c99d9ab455"; 

    try {
        const API = `http://localhost:5043/api/User/${userId}`;

        const request = await fetch(API, { method: "DELETE" }); 

        if (!request.ok) { 
            console.error(`Ошибка при удалении. Статус - ${request.status}`);
            return;
        }

        console.log("Пользователь успешно удален! ");
    } catch (error) {
        console.log("Ошибка сети", error);
    }
}

DeleteUserByID();
UpdateUser();
