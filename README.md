# Space Scrapper VR 🚀

##  🛰️ Overview
Space Scrapper VR is a modernized, performance-optimized scavenger experience built for the Meta Quest ecosystem. While inspired by Valem's "Space Scrapper" concept, this repository represents a complete architectural overhaul, transitioning from "tutorial-level" scripts to a Production-Ready XR Framework built on Unity 6.

## 🛠️ Technical Architecture
The project prioritizes Memory Safety, Physics Stability, and GPU Efficiency for standalone VR hardware.

1. Advanced Interaction & Physics 🧬
   * Interaction Groups (XRI 3.3.1): Implemented a sophisticated hand-priority system to manage "Near-Far" interactor handoffs within tight cockpit confines.
   * Interface-Based Interaction: Decoupled combat and harvesting systems using the IBreakable interface, allowing for infinite scalability of destructible assets without script dependencies.
   * GrabPhysicsStabilizer: Engineered a dynamic CollisionDetectionMode switcher to eliminate "tunneling" issues when objects are released during object grabbing.
   * Logic-Gated Lifecycle: All interactive objects utilize a "Gatekeeper" state-lock pattern to ensure atomic execution of physics events, preventing frame-rate "spikes" during object destruction.

2. Lighting & Rendering (Unity 6 / URP) 💡
   * Adaptive Probe Volumes (APV): Leveraged Unity 6’s APV system with optimized subdivision mapping. High-density (3m) sampling for the cockpit interior ensures accurate light-wrapping on the player's hands and tools.
   * Box-Projected Reflections: Corrected parallax sliding in the cockpit by anchoring reflections to the physical ship geometry, providing a "grounded" sense of presence in VR.
   * Shadow Atlas Optimization: Strategically balanced "Mixed" vs. "Baked" lighting to resolve Shadow Map overflows, maintaining sharp 1:1 shadows on the Quest 3 at 90Hz.
  
3. Professional Audio & UI Systems 🔊
   * Distributed Audio: Abandoned central managers for a localized Emitter Pattern, ensuring 100% HRTF spatial accuracy for every tool and engine component.
   * Unidirectional Data Flow: UI settings (Locomotion, Comfort, Audio) follow a strict UI → ScriptableObject → Logic flow, ensuring persistence and decoupling from scene-specific objects.
  
## 📝 Future Roadmap
* Scrap Collection Loop: Implementing a "Socket-to-Currency" progression system.
* XRI 3.4.0 Datum Migration: Transitioning to centralized Affordance Theme Datums for global visual consistency.
* Modern Input Reader Pattern: Refactoring locomotion to the latest XRI Input Reader abstraction.

## 🤝 Acknowledgments
* Base Concept: Inspired by Valem's "Let's make a VR game".
* Architectural Philosophy: Heavily influenced by the decoupled coding patterns found in CodeMonkey’s "Kitchen Chaos".

## ⚙️ Stack
* Engine: Unity 6 (6000.0.35f1 LTS)
* Toolkit: XR Interaction Toolkit (XRI) 3.3.1
* Pipeline: Universal Render Pipeline (URP)
* Platform: Meta Quest 2 / 3 / Pro (OpenXR)
