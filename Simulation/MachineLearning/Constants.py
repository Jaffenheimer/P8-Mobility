import Helper.SeedGenerator as sg

# TOTAL_TIMESTEPS / (MAX_STEPS * N_ENVS) must be an integer
TOTAL_TIMESTEPS = 1200
MAX_STEPS = 100
N_ENVS = 4 # MAX (amout of core - 1)

RANDOM_MAX_STEPS = MAX_STEPS
SCHEDULE_MAX_STEPS = MAX_STEPS
GREEDY_MAX_STEPS = MAX_STEPS

SUMO_INIT_STEPS = 200

SEED = sg.SEED

REWARD_THRESHOLD = 500

PEEK_INTERVAL = 50
PEEK_LEARN_STEPS = 50

TUNING_STEPS = 1000
TUNING_TOTAL_TIMESTEPS = 4000
TUNING_TRIALS = 100

INPUTFILE = "high_person_low_traffic.sumocfg"
