import numpy as np
from scipy.optimize import linprog
# zadanie
# max {x1+2x2=y1}
# max {2x1+x2=y2}
# x1+x2<=4
#-x1+x2<=2
#-x1+x2>=-1
#x1,x2>=0

# riesenie : identifikacia zloziek idealneho vektora
# 1. max {x1+2x2=y1}
# x1+x2<=4
#-x1+x2<=2
#-x1+x2>=-1
#x1,x2>=0

A_ub = np.array([[1,1],[-1,1],[1,-1]])
b_ub=np.array([4,2,1])
c=np.array([1,2])

res1=linprog(-c,A_ub=A_ub,b_ub=b_ub,bounds=(0,None),method='simplex',options={"disp":True})
print('optimal value:',res1.fun*(-1),'\nx',res1.x)

# 2. max {2x1+x2=y2}
# x1+x2<=4
#-x1+x2<=2
#-x1+x2>=-1
#x1,x2>=0

A_ub = np.array([[1,1],[-1,1],[1,-1]])
b_ub=np.array([4,2,1])
c=np.array([2,1])
res2=linprog(-c,A_ub=A_ub,b_ub=b_ub,bounds=(0,None),method='simplex',options={"disp":True})
print('optimal value:',res2.fun*(-1),'\nx',res2.x)
print()

# riesenie rovnake vahz
yy=(res1.fun*(-1),res2.fun*(-1))
print ("idealny kriterialny vektor yy:",yy)

A_ub = np.array([[-1,-2,-1],[-2,-1,-1],[1,1,0],[-1,1,0],[1,-1,0]])
b_ub=np.array([(-yy[0]),-(yy[1]),4,2,1])
c=np.array([0,0,1])

res=linprog(c,A_ub=A_ub,b_ub=b_ub,bounds=(0,None),method='simplex',options={"disp":True})
C=np.array([[1,2],[2,1]])
y=C@res.x[:2]
print('optimal value:',res.fun,'\nX',res.x,"Y:",y)

# riesenie : generovanie efek. alternativ -vahy od [0.1 0.9] do [0.85 0.15]

for k in np.arange(1,9,0.5):
    A_ub = np.array([[-1, -2, -1/(k/10)], [-2, -1, -1/((10-k)/10)], [1, 1, 0], [-1, 1, 0], [1, -1, 0]])
    b_ub = np.array([(-yy[0]), -(yy[1]), 4, 2, 1])
    c = np.array([0, 0, 1])
    print("\nvahy:",(k/10,(10-k)/10))
    res = linprog(c, A_ub=A_ub, b_ub=b_ub, bounds=(0, None), method='simplex', options={"disp": True})
    C = np.array([[1, 2], [2, 1]])
    y = C@res.x[:2]
    print('optimal value:', res.fun, '\nX', res.x, "Y:", y)
