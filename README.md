<div align="center">

# 🐱 Cat Defense

**Videojuego tower defense desarrollado en Unity y C#**

[![Unity](https://img.shields.io/badge/Unity-2022.3-000000?style=flat&logo=unity&logoColor=white)](https://unity.com/)
[![C#](https://img.shields.io/badge/C%23-.NET-512BD4?style=flat&logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Platform](https://img.shields.io/badge/Platform-Windows-0078D6?style=flat&logo=windows&logoColor=white)](https://www.microsoft.com/windows)
[![Status](https://img.shields.io/badge/Status-Completado-1F7A8C?style=flat)]()

[Descripción](#-sobre-el-proyecto) · [Características](#-características) · [Capturas](#-capturas) · [Cómo jugar](#-cómo-jugar) · [Stack](#-stack-tecnológico)

</div>

---

## 📖 Sobre el proyecto

**Cat Defense** es un videojuego tipo **tower defense** donde el jugador debe defender una base del avance de oleadas enemigas, colocando y mejorando torres estratégicamente a lo largo del mapa.

El proyecto fue desarrollado íntegramente en **Unity** con scripts en **C#** como parte de la formación académica en Ingeniería Informática. Incluye un sistema completo de oleadas, economía de recursos, IA de enemigos y un menú de juego funcional.

> 🎮 **Build jugable disponible** en la carpeta [`Game/`](./Game) del repositorio.

---

## ✨ Características

- 🐈 **Sistema de oleadas** con dificultad progresiva: cada oleada es más numerosa y los enemigos tienen más resistencia.
- 🗺️ **Enemigos con rutas dinámicas**: siguen waypoints predefinidos, obligando al jugador a planificar la colocación de torres.
- 💰 **Economía de recursos**: los enemigos eliminados otorgan recursos que se invierten en colocar o mejorar torres.
- 🎯 **Colocación estratégica**: cada torre tiene un coste, un alcance y una cadencia de disparo distintos.
- 🖥️ **Interfaz de juego completa**: HUD, menú principal, pantalla de victoria/derrota y sistema de pausa.
- ⚙️ **CI configurado**: compilación automática con GitHub Actions.

---

## 🎮 Cómo jugar

### Opción 1 — Descargar la build (recomendado)

1. Ve a la carpeta [`Game/`](./Game).
2. Descarga el archivo `CatDeffense.exe` junto con la carpeta `CatDeffense_Data` y `UnityPlayer.dll`.
3. Ejecuta `CatDeffense.exe`.

> ⚠️ **Windows puede mostrar un aviso de SmartScreen** al tratarse de un ejecutable sin firma digital. Pulsa "Más información" → "Ejecutar de todas formas".

### Opción 2 — Abrir el proyecto en Unity

1. Clona el repositorio:
   ```bash
   git clone https://github.com/albergm02/cat_defense.git
   ```
2. Abre la carpeta `CatDeffense/` desde **Unity Hub**.
3. Usa **Unity 2022.3 LTS** o superior.
4. Abre la escena principal y pulsa *Play*.

---

## 🕹️ Controles

| Acción | Tecla |
| :--- | :--- |
| Seleccionar torre | `1`, `2`, `3` |
| Colocar torre | Clic izquierdo |
| Cancelar colocación | Clic derecho |
| Iniciar oleada | `Espacio` |
| Pausa | `Esc` |

---

## 🛠️ Stack tecnológico

| Categoría | Tecnología |
| :--- | :--- |
| **Motor** | Unity 2022.3 LTS |
| **Lenguaje** | C# (.NET) |
| **Runtime** | Mono (Unity) |
| **Build target** | Windows x86_64 |
| **CI/CD** | GitHub Actions |
| **Control de versiones** | Git · GitHub |

---

## 📂 Estructura del proyecto

```
cat_defense/
├── CatDeffense/              # Proyecto Unity (fuente)
│   ├── Assets/               # Scripts, escenas, prefabs, sprites
│   ├── Packages/             # Dependencias de Unity
│   └── ProjectSettings/      # Configuración del proyecto
├── Game/                     # Build ejecutable para Windows
│   ├── CatDeffense.exe
│   ├── CatDeffense_Data/
│   └── UnityPlayer.dll
├── docs/                     # Documentación y capturas
│   └── screenshots/
├── CatDeffense_Video.mp4     # Vídeo demostrativo
├── Informe _CatDeffense.pdf  # Informe técnico del proyecto
└── README.md
```

---

## 📚 Documentación

El proyecto incluye un informe técnico completo con:

- Análisis de requisitos y casos de uso.
- Diseño de mecánicas y balanceo.
- Decisiones técnicas y arquitectura del código.
- Conclusiones y posibles mejoras.

📄 Disponible en [`Informe _CatDeffense.pdf`](./Informe%20_CatDeffense.pdf).


## 👤 Autor

**Alberto García Martín**

- 🌐 Portfolio: [albertogm.dev](https://albertogm.dev)
- 💼 LinkedIn: [in/albertogarciamartin](https://www.linkedin.com/in/albertogarciamartin/)
- 🐙 GitHub: [@albergm02](https://github.com/albergm02)
- 📧 Email: alberto.g.m.0214@gmail.com

---

<div align="center">

**⭐ Si este proyecto te resulta interesante, considera darle una estrella.**

</div>
