import numpy as np
from scipy.optimize import linprog

# ciel {x1+2x2=y1 y1=8}
# ciel {x2=y2} y2=1
# 2x1+x2<=6
# x1<=2
# x1,x2>=0
# riesenie : rovnake vahy pozor ohranicenia = uloha je min

A_ub = np.array([[-30,-15,-1,0],
                 [-1,-1,0,-1],
                 [4, 3, 0, 0 ],
                 [3, 1, 0, 0],
                 [0,-1,0,0]])

b_ub = np.array([-2100,-100,240, 150,-10])


c = np.array([0, 0, 1, 1])

res = linprog(c, A_ub=A_ub, b_ub=b_ub, bounds=(0, None), method='simplex')
print('optimal value', round(res.fun, 2), '\nX', res.x.round(2))
print()

for k in range(101):
    c = np.array([0, 0, k / 100,(100 - k) / 100,])
    # 2: od indexu 2 po koniec
    print("vahy:", c[2:])
    res = linprog(c, A_ub=A_ub, b_ub=b_ub, bounds=(0, None), method='simplex')
    C = np.array([[30, 15], [1, 1]])
    print("haha:",res.x)
    # len po index jedna lebo tam sa nachadzaju ocenenia rozhodovacich premennych
    y = C @ res.x[:2]
    print('Optimal ,value:', round(res.fun, 2), '\nX', res.x.round(2), "Y:", y)
