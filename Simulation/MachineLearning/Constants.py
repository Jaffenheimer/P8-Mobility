import Helper.SeedGenerator as sg
import multiprocessing
from random import randint

# TOTAL_TIMESTEPS / (MAX_STEPS * N_ENVS) must be an integer
TOTAL_TIMESTEPS = 1000
MAX_STEPS = 1000
N_ENVS = 4 # MAX (amout of core - 1)

RANDOM_MAX_STEPS = MAX_STEPS
SCHEDULE_MAX_STEPS = MAX_STEPS
GREEDY_MAX_STEPS = MAX_STEPS

UPDATEPOLICY = 200

SUMO_INIT_STEPS = 200

SEED = sg.SEED
#SEED = 12345
SEEDS = [18467,0, 66312,0, 28134,0, 17258,0, 50199,0,0,0]

REWARD_THRESHOLD = 500

PEEK_INTERVAL = 50
PEEK_LEARN_STEPS = 400/N_ENVS

TUNING_STEPS = 1000
TUNING_TOTAL_TIMESTEPS = 4000
TUNING_TRIALS = 100

INPUTFILE = "low_person_low_traffic.sumocfg"

NUM_CORES = multiprocessing.cpu_count()