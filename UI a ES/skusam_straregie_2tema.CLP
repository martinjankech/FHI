(deffacts ovoce
(ovoce jablko hruska banan pomaranc)
(ovoce jablko hruska)

)

(defrule strategie
(or(ovoce jablko hruska ?x)
(ovoce jablko hruska))

=>
(printout t "splnene "   crlf)
)