from stable_baselines3 import PPO, A2C
from sb3_contrib import RecurrentPPO, TRPO
from Helper.PlotDiagram import PlotAveragePeopleAtBusstopsMultiple, PlotAverageWaitTimeMultiple
from RandomVersion import RandomVersion
from GreedyFastVersion import GreedyFastVersion
from ScheduleVersion import ScheduleVersion
import time
import Model


def CombinedTest():

    start_time = time.time()

    data_PPO = Model.run(PPO, "PPO", "MlpPolicy")
    
    data_recurrent_PPO = Model.run(RecurrentPPO, "Recurrent PPO", "MlpLstmPolicy")

    data_A2C = Model.run(A2C, "A2C", "MlpPolicy")

    data_TRPO = Model.run(TRPO, "TRPO", "MlpPolicy")

    data_random = RandomVersion()

    data_schedule = ScheduleVersion()

    data_greedy = GreedyFastVersion()

    print(f"Total time: {time.time() - start_time:.2f} seconds")

    PlotAverageWaitTimeMultiple(("Random", data_random), ("Greedy", data_greedy), ("PPO", data_PPO), ("Recurrent PPO", data_recurrent_PPO),
                                ("TRPO", data_TRPO), ("A2C", data_A2C), ("Schedule", data_schedule))

    PlotAveragePeopleAtBusstopsMultiple(("Random", data_random), ("Greedy", data_greedy), ("PPO", data_PPO), ("Recurrent PPO", data_recurrent_PPO),
                                        ("TRPO", data_TRPO), ("A2C", data_A2C), ("Schedule", data_schedule))


if __name__ == "__main__":
    CombinedTest()
