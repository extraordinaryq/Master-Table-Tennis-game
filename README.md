#  Pingpong-human Table Tennis Game
**Deep Reinforcement Learning-based Training for Virtual Table Tennis Agent**\
Daqi Jiang, Hock Soon Seah*, Budianto Tandianus, Yiliang Sui, Hong Wang
<br>
Accept by International Conference on Virtual Reality (ICVR), 2024
<br>Supervised by [Prof Seah Hock Soon](https://dr.ntu.edu.sg/cris/rp/rp00345), [Budianto Tandianus](https://www.singaporetech.edu.sg/directory/professional-officers/budianto-tandianus)
<br>High-resolution demo video is available on [YouTube](https://www.youtube.com/playlist?list=PL2UD_JyvqMzfo0Z4aaf3IONPuUPtUIifV)

<details>
  <summary>
  <font size="+1">Abstract</font>
  </summary>
The advent of Virtual Reality (VR) has revolutionized various fields, notably by merging digital and physical realities, and has significantly influenced sports like table tennis. This paper introduces a novel VR-based table tennis game, employing Deep Reinforcement Learning (DRL) algorithms to enhance player experience and skill development of the agents. The game features a VR table tennis scene with human models, where the movements are controlled by a neural network trained through DRL. For the ball itself, we employ a physics engine to calculate its trajectory considering collision and gravity. The core contribution of this paper is the implementation of DRL algorithms to train the neural network and simulate the behavior of a table tennis player. Comparison experiment with different algorithms validates the efficacy of the proposed method. Additionally, the game includes human models with varied characteristics like speed, force, and active reach, which are trained separately to explore optimal strategies for playing against diverse player profiles. Two training programs including skill mastery and skill generalization are set up to explore the optimal training strategy, which offers valuable lessons in real world for both amateurs and professional athletes.
</details>
<div align="left">
    <a href="./">
        <img src="./pong.gif" width="60%"/>
    </a>
</div>

## About
**Pingpong-human Table Tennis Game** is a multi-agent reinforcement learning environment built on [Unity ML-Agents](https://unity.com/products/machine-learning-agents).

> **Version:** Up-to-date with ML-Agents Release 21
## Contents
1. [Getting started](#getting-started)
2. [Training](#training)
3. [PPO](#PPO)
4. [Self-play](#self-play)
5. [Environment description](#environment-description)
6. [Human model setup](#human-model-setup)
6. [Baselines](#baselines)

## Getting Started
1. Install the Unity ML-Agents toolkit (Release 21+) by following the [installation instructions](https://github.com/Unity-Technologies/ml-agents/tree/release_21).
2. Download or clone this repo containing the `Pingpong-human` Unity project.
3. Open the `Pingpong-human` project in Unity (Unity Hub → Projects → Add → Select root folder for this repo).
4. Load the `TableTennis human inference` scene (Project panel → Assets → `TableTennis human inference.unity`).
5. Click the ▶ button at the top of the window. This will run the agent in inference mode using the provided baseline model.

## Training

1. Load the `TableTennis human train` scene (Project panel → Assets → `TableTennis human train.unity`).
2. If you previously changed Behavior Type to `Heuristic Only`, ensure that the Behavior Type is set back to `Default` .
2. Activate the virtual environment containing your installation of `ml-agents`.
3. Make a copy of the [provided training config file](Config/) in a convenient working directory.
4. Run from the command line `mlagents-learn <path to config file> --run-id=<some_id> --time-scale=1`
    - Replace `<path to config file>` with the actual path to the file in Step 4
5. When you see the message "Start training by pressing the Play button in the Unity Editor", click ▶ within the Unity GUI.
6. From another terminal window, navigate to the same directory you ran Step 5 from, and run `tensorboard --logdir results` to observe the training process. 

For more detailed instructions, check the [ML-Agents getting started guide](https://github.com/Unity-Technologies/ml-agents/blob/release_21/docs/Getting-Started.md).

## PPO
To enable PPO algorithm:
1. Set both Left and Right Agent Team ID to 0.
2. Include the PPO hyperparameter hierarchy in your trainer config file, or use the provided file in `Config/pingpong_human.yaml` ([ML-Agents Documentation](https://github.com/Unity-Technologies/ml-agents/blob/main/docs/Learning-Environment-Design-Agents.md#teams-for-adversarial-scenarios))
3. Set the reward function by switching `Rewardfunction` to `PPO` in `TableTennisEnvControl.cs`.

## Self-Play
To enable self-play algorithm:
1. Set either Left or Right Agent Team ID to 1.
2. Include the self-play hyperparameter hierarchy in your trainer config file, or use the provided file in `Config/pingpong_human_Self.yaml` ([ML-Agents Documentation](https://github.com/Unity-Technologies/ml-agents/blob/main/docs/Learning-Environment-Design-Agents.md#teams-for-adversarial-scenarios))
3. Set the reward function by switching `Rewardfunction` to `SelfPlay` in `TableTennisEnvControl.cs`.

## Environment Description
**Rule:** Exactly same as table tennis in real life.

**Action space:**

12 continuous action branches:
- translation (3D) of the human model
- rotation increments (3D) of the shoulder joint
- rotation increment (1D) of the elbow joint
- rotation increments (2D) of the wrist joint
- force (3D) of hitting the ball

**Observation space:**

Total size: 16
- speed (3D) and position (3D) of the ball
- position (3D) of the human model
- distance (1D) between ball and racket
- rotation angles (3D) of the shoulder joint
- rotation angle (1D) of the elbow joint
- rotation angles (2D) of the wrist joint

![Table Tennis Game](https://github.com/extraordinaryq/Master-Table-Tennis-game/blob/master/network.svg)
<br>
**Reward function:**

The project contains some examples of how the reward function can be defined.
The PPO algorithm gives a +0.25 reward each time the agent receives the ball
and +1 reward each time the agent hits the ball to opponent side. 
The self-play algorithm gives a +1 reward for winner and -1 penalty for loser.

## Human model setup

Switch different human model by changing `Humanmodel` value in `HumanAi.cs`.
<br>Specific parameters of different speed, strength, and active reach:

| Model     | Speed | Force | Active reach | Character            |
|-----------|-------|-------|--------------|----------------------|
| Model   1 | 100%  | 100%  | 100%         | Ordinary adult       |
| Model   2 | 150%  | 80%   | 100%         | Thin, flexible       |
| Model   3 | 80%   | 160%  | 100%         | Strong, bulky        |
| Model   4 | 150%  | 160%  | 100%         | Professional athlete |
| Model   5 | 100%  | 80%   | 60%          | Child                |

## Baselines
The following baselines are included:
- `Pingpong-human_PPO_model1.onnx` - Model1 trained using PPO in 10M steps
- `Pingpong-human_SelfPlay_model4.onnx` - Model4 trained using PPO with Self-Play in 20M steps

## Acknowledgement

The code base is built with [Ultimate Volleyball](https://www.gocoder.one/blog/competitive-self-play-unity-ml-agents/).

Thanks for the great implementations! 

## Citation

If our code or models help your work, please cite our paper:
```BibTeX
@InProceedings{Seah_2024_ICVR,
    author    = {Jiang, Daqi and Seah, Hock Soon and Tandianus, Budianto and Sui, Yiliang and Wang, Hong},
    title     = {Deep Reinforcement Learning-based Training for Virtual Table Tennis Agent},
    booktitle = {International Conference on Virtual Reality (ICVR)},
    month     = {July},
    year      = {2024},
}
```