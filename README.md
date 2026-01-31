
<h1 align="center">🏗️ Multi-Agent Co-ordinated Task Planning in Construction</h1>



<p align="center">
  <img src="Demos/Demo.gif" width="800"/>
</p>

<p align="center">
  <em>Unity-based multi-agent construction simulation</em>
</p>

---

<p align="center">
A collaborative research and simulation project integrating  
<strong>multi-agent systems</strong>, <strong>computer vision</strong>, and <strong>AI-based planning</strong>  
to model, analyse, and visualise coordinated task execution in a construction-site environment.
</p>

<p align="center">
🎮 Unity Simulation &nbsp;•&nbsp; 🤖 AI / ML (Google Colab) &nbsp;•&nbsp; 🧠 Task Planning Algorithms
</p>

---

## 📁 Repository Structure

```
├── Assets/                         # Unity assets (scripts, scenes, prefabs, materials)
├── Packages/                      # Unity package dependencies
├── ProjectSettings/               # Unity project configuration
├── YOLOv8_train.ipynb             # Colab notebook – YOLO fine-tuning
├── Multi agent planning - Simulation.ipynb
│                                  # Colab notebook – planning & simulation logic
├── Demos/                          # Demo GIFs for README
│   ├── Demo.gif
│   └── Democolab.gif
├── .gitignore
└── README.md
```

---

## 🎮 Opening the Unity Simulation

### Prerequisites
- **Unity Hub**
- **Unity Editor (LTS version recommended, as used by the team)**

### Steps
1. Open **Unity Hub**
2. Click **Open Project**
3. Select the **root folder of this repository**
4. Unity will automatically load:
   - `Assets`
   - `Packages`
   - `ProjectSettings`

> ⚠️ **Note**  
> The `Library/` folder is intentionally excluded from version control.  
> Unity regenerates it automatically on first launch.

---

## Architecture Diagram
<p align="center">
  <img width="568" height="525" alt="image" src="https://github.com/user-attachments/assets/9ec775a3-bc7c-4705-a3f9-6099a3492ced" />
</p>


## 🤖 AI / ML Work (Google Colab)

This repository includes **Google Colab notebooks** developed collaboratively by the team and uploaded directly to the repository root.

### Included Notebooks
- **YOLOv8_train.ipynb**  
  Fine-tuning YOLO for object detection, including dataset loading, training, and evaluation.

- **Multi agent planning - Simulation.ipynb**  
  Task-planning logic, simulation workflow, and algorithmic analysis.

### How to Use
1. Open the required `.ipynb` file in **Google Colab**
2. Follow the documented notebook cells for:
   - Dataset preparation
   - Model training
   - Inference and evaluation

---

## 🤖 AI / Colab Simulation Demo

<p align="center">
  <img src="Demos/Democolab.gif" width="800"/>
</p>

<p align="center">
  <em>Algorithmic task planning and simulation executed in Google Colab</em>
</p>

---

## 🔄 Task Planning & Coordination Logic

Construction tasks are modeled as a **Directed Acyclic Graph (DAG)** to enforce dependency constraints and correct execution order. Multiple robots dynamically select available tasks and execute them in parallel whenever dependencies allow. Each agent navigates a 2D grid using Manhattan movement while continuously checking for occupancy conflicts. A **priority-based arbitration mechanism** resolves deadlocks by allowing higher-priority agents to proceed. Upon task completion, the DAG is updated and dependent tasks are released, enabling efficient and coordinated execution.

---

## 📊 Summary Table (High-Level System Design)

| Component            | Function                 | Algorithm Used                     |
|----------------------|--------------------------|------------------------------------|
| Task Modeling        | Dependency handling      | Directed Acyclic Graph (DAG)        |
| Task Release         | Availability check       | Predecessor Verification           |
| Scheduling           | Execution ordering       | Dynamic DAG Scheduling             |
| Task Assignment      | Robot selection          | Greedy Allocation                  |
| Navigation           | Movement                 | Manhattan Heuristic                |
| Collision Detection  | Safety                   | Occupancy Grid Check               |
| Conflict Resolution  | Deadlock breaking        | Priority Arbitration               |
| Coordination         | Teamwork                 | Decentralized Control              |
| State Update         | Task completion tracking | Event-Driven Update                |
| Evaluation           | Performance analysis     | Statistical Aggregation            |

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
- Task-planning algorithms and analytical evaluation

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
