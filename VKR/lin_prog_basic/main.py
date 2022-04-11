import numpy as np
import scipy as cp
from scipy.optimize import linprog

# ciel (2x1+x2=y1 y1=10)
# ciel (2x1+3x2=y2 y2=12)
# x1+3x2<=9
# 2x1+x2<=6
# x1,x2>=0
# riesenie:rovnake vahy , pozor ohranicenia "=",pozor uloha je min
A_ub = np.array([[1, 3, 0, 0, 0, 0],
                 [2, 1, 0, 0, 0, 0]])
b_ub=np.array([9, 6])
A_eq = np.array([[2, 1, 1, -1, 0, 0],
                 [2, 3, 0, 0, 1, -1]])

b_eq = np.array([10, 12])
c= np.array([0, 0, 1, 1, 1, 1])

res = linprog(c, A_ub=A_ub, b_ub=b_ub, A_eq = A_eq, b_eq=b_eq, bounds=(0,None),method='simplex', options={"disp": True})
print('optimal value:', res.fun, '\nX:', res.x)
print()