-- Bei Prüfbedingungen Werkzeug mit Typen QuchickStep die Einstellung "als Quickstep prüfen" aktivieren
update cond_pow set ToolTypeOverride = 6 where locpowid in (select lp.locpowid from locpow lp inner join pow_tool pt on pt.seqid = lp.powid inner join model m on m.modelid = pt.modelid where m.typeid = 6);

-- Werkzeugmodelle mit dem Typen Quickstep auf EC-Schrauber stellen
update model set typeid = 4 where typeid = 6;

commit;