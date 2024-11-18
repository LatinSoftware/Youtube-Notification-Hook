document.addEventListener("DOMContentLoaded", () => {
    initializeCopyButton();
    initializeNotificationButton();
    setupSignalRConnection();
});

/**
 * Configura el botón para copiar texto al portapapeles.
 */
function initializeCopyButton() {
    const copyButton = document.getElementById('copyButton');
    const input = document.getElementById('suscription-link-input');

    if (!copyButton || !input) {
        console.warn("Botón de copia o input no encontrados.");
        return;
    }

    copyButton.addEventListener('click', () => {
        input.select();
        navigator.clipboard.writeText(input.value)
        copyButton.innerText = "Copiado!"
        input.value = ''
        setTimeout(() =>
        {
            copyButton.innerText = "Copiar"
        }, 3000)
    });
}

/**
 * Inicializa el botón de notificaciones.
 */
function initializeNotificationButton() {
    const notificationButton = document.getElementById("notification-btn");
    const notificationText = document.getElementById('notificationText');
    let notificationsEnabled = false;

    if (!notificationButton || !notificationText) {
        console.warn("Botón o texto de notificaciones no encontrados.");
        return;
    }

    //notificationButton.addEventListener('click', () => {
    //    if (!notificationsEnabled) {
    //        updateNotificationButton(notificationButton, notificationText, notificationsEnabled);
    //    }
    //});

    Notification.requestPermission().then((permission) => {
        console.log("Resultado de permisos de notificación:", { permission });
        notificationsEnabled = (permission === 'granted');
        updateNotificationButton(notificationButton, notificationText, notificationsEnabled);
    }).catch((error) => {
        console.error("Error al solicitar permisos de notificación:", error);
    });
}

/**
 * Actualiza el estado del botón de notificaciones.
 */
function updateNotificationButton(button, textElement, isEnabled) {
    if (isEnabled) {
        button.classList.add('active');
        textElement.textContent = 'Activadas';
    } else {
        button.classList.remove('active');
        textElement.textContent = 'Desactivadas';
    }
}

/**
 * Configura la conexión SignalR.
 */
function setupSignalRConnection() {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/youtube-notification")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ReceiveMessage", (channel, link) => {
        handleIncomingMessage(channel, link);
    });

    connection.start()
        .then(() => console.log("Conexión SignalR establecida"))
        .catch((error) => console.error("Error al conectar SignalR:", error));
}

/**
 * Maneja los mensajes entrantes desde SignalR.
 */
function handleIncomingMessage(channel, link) {
    const suscriptionLinkInput = document.getElementById("suscription-link-input");

    if (!suscriptionLinkInput) {
        console.warn("Input para el enlace de suscripción no encontrado.");
        return;
    }

    console.log(`Mensaje recibido del canal: ${channel}, URL: ${link}`);

    // Mostrar una notificación al usuario
    if (Notification.permission === "granted") {
        const notification = new Notification(`Canal ${channel} ha subido nuevo video`, {
            body: `Nuevo enlace disponible: ${link}`
        });
    }

    // Actualizar el valor del input
    suscriptionLinkInput.value = link;
}
