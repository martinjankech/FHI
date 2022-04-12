(deffacts duck
(bachelor Dicky)
(bachelor Dopey)
(bachelor Dopey Mallar)
(bachelor Dinky Dopey Mallard))

(defrule dating_ducks
(bachelor Dopey $?last )
=>
(printout t "Date " "Dopey " ?last crlf))

