import numpy as np
from scipy.optimize import linprog

# ciel {x1+2x2=y1 y1=8}
# ciel {x2=y2} y2=1
# 2x1+x2<=6
# x1<=2
# x1,x2>=0
#riesenie : rovnake vahy pozor ohranicenia = uloha je min

A_ub = np.array([[2, 1, 0, 0, 0, 0],
                [1, 0, 0, 0, 0, 0]])

b_ub = np.array([6, 2])

A_eq=np.array([[1, 2, 1, -1, 0, 0],
               [1, -1, 0, 0, 1, -1]])
b_eq = np.array([8,1])
c=np.array([0, 0, 1, 1, 1, 1])


res = linprog(c,A_ub=A_ub,b_ub=b_ub,A_eq=A_eq,b_eq=b_eq,bounds=(0,None),method='simplex')
print('optimal value',round(res.fun,2),'\nX',res.x.round(2))
print()

for k in range(11):
    c=np.array([0,0,k/10,k/10,(10-k)/10,(10-k)/10])
    # 2: od indexu 2 po koniec
    print("vahy:",c[2:])
    res = linprog(c, A_ub=A_ub, b_ub=b_ub, A_eq=A_eq, b_eq=b_eq, bounds=(0, None), method='simplex')
    C=np.array([[1,2],[1,-1]])
    y= C@res.x[:2]
    print('Optimal ,value:',round(res.fun,2),'\nX',res.x.round(2),"Y:",y)