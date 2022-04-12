import numpy as np
from scipy.optimize import linprog
# zadanie
# max {x1}
# max {-3x1+x2}
# x1+x2<=4
#-3x1+2x2<=6
#3x1+x2<=9
#x1,x2>=0

# riesenie pre samostannte rovnake vahy
A_ub = np.array([[1,1],[-3,2],[3,1]])
b_ub=np.array([4,6,9])
c1=np.array([1,0])
c2=np.array([-3,1])
c=1*np.array([1,0])+1*np.array([-3,1])
print("agregovana UF:",c)
res=linprog(-c,A_ub=A_ub,b_ub=b_ub,bounds=(0,None),method='simplex',options={"disp":True})
C = np.array([[1, 0], [-3, 1]])
y = C @ res.x[:2]
print('optimal value:',res.fun*(-1),'\nx',res.x,"Y",y)
print()

#generovanie pre rozne vahy
A_ub = np.array([[1,1],[-3,2],[3,1]])
b_ub=np.array([4,6,9])
c1=np.array([1,0])
c2=np.array([-3,1])
for k in np.arange(0,10.5,0.5):
    c=k/10*np.array(c1)+(10-k)/10*np.array(c2)
    print("vahy",k/10,(10-k)/10)
    print("agregovana UF:",c)
    res = linprog(-c, A_ub=A_ub, b_ub=b_ub, bounds=(0, None), method='simplex', options={"disp": True})
    C = np.array([[1, 0], [-3, 1]])
    y = C @ res.x[:2]
    print('optimal value:', res.fun * (-1), '\nx', res.x, "Y", y)
    print()
