import numpy as np
from scipy.optimize import linprog

# ciel {30x1+15x2>=y1 y1=2100}
# ciel {x1+x2=y2} y2>=100
# 4x1+3x2<=240
# 3x1+x2<=150
# x2>=10
# x1,x2>=0
A_ub = np.array([[-30,-15,-1,0],
                 [-1,-1,0,-1],
                 [4, 3, 0, 0 ],
                 [3, 1, 0, 0],
                 [0,-1,0,0]])

b_ub = np.array([-2100,-100,240, 150,-10])


c = np.array([0, 0, 1, 1])

res = linprog(c, A_ub=A_ub, b_ub=b_ub, bounds=(0, None), method='simplex')
C = np.array([[30, 15], [1, 1]])
y = C @ res.x[:2]
print('vahy λ1=1 a λ2=1')
print('hodnota ucelovej funkcie pre váhy 1 a 1 je:', round(res.fun, 2), '\nx* =', res.x[:2].round(2),'\ny* = ',y,'\nd(d1-;d2-)=',res.x[2:].round(2))
print()
print('riesenie pre vahy λ1=0.03 a λ2=0.97')
c = np.array([0, 0, 0.03, 0.97])

res = linprog(c, A_ub=A_ub, b_ub=b_ub, bounds=(0, None), method='simplex')
y = C @ res.x[:2]
print('hodnota ucelovej funkcie pre váhy λ1=0.03 a λ2=0.97  je:', round(res.fun, 2), '\nx*', res.x[:2].round(2),'\ny* = ',y,'\nd(d1-;d2-)=',res.x[2:].round(2))
print()

print('generovanie pre rôzne vahy od 0.01 po 1 ')
for k in range(101):
    c = np.array([0, 0, k / 100,(100 - k) / 100,])
    # 2: od indexu 2 po koniec
    print("vahy:", c[2:])
    res = linprog(c, A_ub=A_ub, b_ub=b_ub, bounds=(0, None), method='simplex')
    C = np.array([[30, 15], [1, 1]])
    # len po index jedna lebo tam sa nachadzaju ocenenia rozhodovacich premennych
    y = C @ res.x[:2]
    print('Optimal ,value:', round(res.fun, 2), '\nX', res.x.round(2), "Y:", y)
