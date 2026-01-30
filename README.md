# 🏗️ Multi-Agent Co-ordinated Task Planning in Construction

A collaborative research and simulation project that integrates **multi-agent systems**, **computer vision**, and **AI-based planning** to model, analyse, and visualise coordinated task execution in a construction-site environment.

This repository represents the **combined work of the entire team**, including:
- 🎮 A **Unity-based multi-agent construction simulation**
- 🤖 **AI / ML experimentation using Google Colab** (YOLO fine-tuning and other models)
- 🧠 Algorithmic approaches for task coordination and planning

---

## 📁 Repository Structure

```
├── Assets/                         # Unity assets (scripts, scenes, prefabs, materials)
├── Packages/                      # Unity package dependencies
├── ProjectSettings/               # Unity project configuration
├── YOLOv8_train.ipynb             # Google Colab notebook – YOLO fine-tuning
├── Multi agent planning - Simulation.ipynb
│                                  # Colab notebook – planning & simulation logic
├── .gitignore
└── README.md
```

---

## 🎮 Opening the Unity Simulation

### Prerequisites
- **Unity Hub**
- **Unity Editor (LTS version recommended, as used by the team)**

### Steps to Open
1. Open **Unity Hub**
2. Click **Open Project**
3. Select the **root folder of this repository**
4. Unity will automatically load:
   - `Assets`
   - `Packages`
   - `ProjectSettings`

> ⚠️ Important  
> The `Library/` folder is intentionally excluded from version control.  
> Unity regenerates it automatically on first launch.

---

## 🤖 AI / ML Work (Google Colab Notebooks)

This repository includes **Google Colab notebooks** developed collaboratively by the team and uploaded directly to the repository root.

### Included Notebooks
- **YOLOv8_train.ipynb**
  - Fine-tuning YOLO for object detection
  - Dataset loading, training, and evaluation

- **Multi agent planning - Simulation.ipynb**
  - Algorithmic task planning
  - Simulation logic and analysis
  - Model experimentation and results

### How to Use
1. Open the required `.ipynb` file in **Google Colab**
2. Follow the instructions within the notebook cells for:
   - Dataset preparation
   - Model training
   - Inference and evaluation
  
### Colab Simulation

<img width="741" height="752" alt="image" src="https://github.com/user-attachments/assets/34fa4a92-7253-4fd9-94e7-4cdd299726f0" />

<img width="743" height="748" alt="image" src="https://github.com/user-attachments/assets/8acecfcf-19c0-4ced-9595-9c7dcac71a9a" />

Construction tasks are modeled as a **Directed Acyclic Graph (DAG)**, ensuring correct execution order through dependency constraints. Multiple robots dynamically select available tasks and execute them in parallel whenever possible. Each agent navigates a 2D grid using Manhattan movement while continuously checking for occupancy conflicts. In case of conflicts, a **priority-based arbitration mechanism** resolves deadlocks by allowing higher-priority agents to proceed. Upon task completion, the system updates the DAG and releases agents to take on newly available tasks, enabling efficient and coordinated execution.

### 📊 Summary Table (High-Level System Design)

| Component            | Function                 | Algorithm Used                     |
|----------------------|--------------------------|------------------------------------|
| Task Modeling        | Dependency handling      | Directed Acyclic Graph (DAG)        |
| Task Release         | Availability check       | Predecessor Verification           |
| Scheduling           | Execution ordering       | Dynamic DAG Scheduling             |
| Task Assignment      | Robot selection          | Greedy Allocation                  |
| Navigation            | Movement                 | Manhattan Heuristic                |
| Collision Detection  | Safety                   | Occupancy Grid Check               |
| Conflict Resolution  | Deadlock breaking        | Priority Arbitration               |
| Coordination         | Teamwork                 | Decentralized Control              |
| State Update         | Task completion tracking | Event-Driven Update                |
| Evaluation           | Performance analysis     | Statistical Aggregation            |

---





> Each notebook is self-contained and documented within the code cells.

---

## 🧩 System Overview

### Unity Simulation
- Multi-agent coordination in a construction environment
- Zone-based task allocation and execution
- Agent movement, interaction, and control logic
- Designed for extensibility and future AI integration

### AI / ML Pipeline
- Vision-based perception using YOLO
- Algorithmic decision-making and planning
- Offline experimentation via Colab with scope for real-time integration

---

## 👥 Team Collaboration

This repository is a **collaborative effort**, combining contributions from all team members:
- Unity simulation and agent logic
- AI / ML model development and experimentation
- Planning algorithms and analytical evaluation

All files have been merged carefully to **preserve each member’s work** and maintain repository integrity.

---

## 📌 Notes for Evaluators

- The Unity project opens directly from the repository root
- All AI / ML experimentation is documented via Google Colab notebooks
- Standard Unity version-control best practices are followed
- Auto-generated Unity folders are excluded for reproducibility

---

## 📜 License

This project is intended for **academic and research purposes**.
