from scipy.sparse import random
from scipy import stats
import numpy as np
class CustomRandomState(np.random.RandomState):
    def randint(self, k):
        i = np.random.randint(k)
        return i - i % 2
np.random.seed(12345)
rs = CustomRandomState()
rvs = stats.poisson(25, loc=10).rvs
S = random(10, 10, density=0.25, random_state=rs, data_rvs=rvs)
print(S.A)