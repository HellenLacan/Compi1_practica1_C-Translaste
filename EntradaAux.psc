


Conteiner Motor[

	Pub Str Piston1 = "Piston 1, listo";
	Pub Str Piston2 = "Piston 2, listo";
	Pro Num cod_culata  = 5+5+10;

	Bool encendido = fals;
	Dec num_pistones= 10.0/2.0 + 5.0 ;
	Num capacidad;
	Pri Str  Codigo_motor = "#AF@#$@#$@$"; 
	Pub Num cilindros;
	
	Motor(Bool encendido, Num capacidad, Num cilindros):Pub[
		prop.encendido = encendido;
		prop.capacidad = capacidad;
		prop.cilindros = cilindros;		
	]

	Motor():Pub[
		prop.encendido = trus;
		prop.capacidad = 50+50-100+20*2+(200/4);
		prop.cilindros = 100*2+(50*-1);
		Codigo_motor = "Cambio el codigo de motor";
		cod_culata= 201331324;
	]

	Pub calcularMet() :vac [
		print("Llamada a metodo sin parametros.");
		Num res = 10*2 + cod_culata;
		print("EL resultado 1 es => "+res);					
	]

	Pub calcularMet2(Dec num1,Num num2,Str cad) :vac [
		print("Llamada a metodo con parametros.");
		Num res = 10*2 + cod_culata+num2;
		print("Parametro 1 => "+num1);
		print("Parametro 2 => "+num2);
		print("Parametro 3 => "+cad);
		print("EL resultado 1 es => "+res);						
	]

	Pub validarFun(): Str[
		print("Llamada a funcion sin parametros.");
		Str res = Piston1 + Piston2;
		print("EL resultado de concatenacion es => "+res);
		ret res;
	]

	Pub validarFun2(Num val, Num val2, Bool var): Str[
		
		sif ( var and not var )[
			print("Este if nunca entra");
		]sifnot[

			sif (var)[
				print("entro por que envio"+var);
			]sifnot[
				print("no entro por que envio"+var);
			]
		]

		ret metRetorno() + (val*val2);
	]


	Pub metRetorno(): Num [
		ret 50;
	]

	Pub metRetorno2(): Num [
		ret 10;
	]

	Pub comprobarWhile( Num val, Num val2): Num[

		whs ( val Gt 0 and val2 Eqs 0)[
			print(" Valor de val es => "+val);
			val2 = val;
			hc[
				Print(" Valor de val2 es => " + val2);
				val2=val2-1;				
			] whs ( val Gt 0);
			val = val-1;		
		]

		ret metRetorno();
	]

	Pub comprobarFor(Num it1, Dec it2 ): Dec[

		Dec Ret=0.0*0.0;
		sfr(Num i =0 ; i Lt it1 ; inc) [
			print("Iteracion de for1 es => "+i);
			sfr(Dec j = it2 ; j Gt -1 ; dec) [
				print("Iteracion de for2 es => "+j);
				Ret = Ret + j;
			]
		]

		ret Ret;			
	]

	Pub comprobarSwitch(Num op): vac[


		sif(-1 Eq -1 )[
			select(op +0 +(10*0)) [
			cas 1 :
				print("Metodo sin parametros");
				calcularMet();
				brk;
			cas 2:
				print("Metodo  con parametros");
				calcularMet2(5.5, 10, "Hola mundo");
				brk;
			cas 3:
				print("Funcion sin parametros");
				print(" ---- " + validarFun());
				brk;
			cas 4:
				print("Funcion con parametros e if");
				print(" ---- " + validarFun(10,5,trus and not fals));
				brk;
			cas 5:
				print(" Funcion del While y Do-while");
				print(" ------- "+ comprobarWhile(10,5)); 
				brk;
			def:
				print(" funcion del for ");
				print(" ----------- "+comprobarFor(10,3.0));
				brk;
			]		
		]

		sifnot[
			ret;
		]
	]

]



Conteiner Carro[

	
	Carro() : Pub[
		Motor mot = inst Motor();
		Num  i = 0 ;
		whs ( i Gte 0 and i Lte 8 and trus)[
			sif(i Eq 7)[
				brk;
			]
			mot.comprobarSwitch(i);				
			i=i+1;			
		]

		print("Variables globales isntaica 1");
		print( "Variable 1  =>  "+mot.encendido);
		print( "Variable 2  =>  "+mot.capacidad);
		print( "Variable 3  =>  "+mot.cilindros);
		print( "Variable 4  =>  "+mot.Codigo_motor);
		print( "Variable 5  =>  "+mot.cod_culata);


		mot = inst Motor(trus,mot.metRetorno2(),-5);

		print("Variables globales isntaica 2");
		print( "Variable 1  =>  "+mot.encendido);
		print( "Variable 2  =>  "+mot.capacidad);
		print( "Variable 3  =>  "+mot.cilindros);
		print( "Variable 4  =>  "+mot.Codigo_motor);
		print( "Variable 5  =>  "+mot.cod_culata);
	]

	
]