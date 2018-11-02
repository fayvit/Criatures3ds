public class UpDeNivel 
{
    public static void calculaUpDeNivel(int esseNivel, Atributos A, bool total = false)
    {
        //float[] taxas = new float[5]{0.15f,0.25f,0.25f,0.15f,0.2f};//taxas fantasia para testes;
        float[] taxas = new float[5]{
            A.PV.Taxa,
            A.PE.Taxa,
            A.Ataque.Taxa,
            A.Defesa.Taxa,
            A.Poder.Taxa
        };
        float[] pontosAcumulados = new float[5] { 1, 1, 1, 1, 1 };
        float[] antigoAcumulado = new float[5] { 0, 0, 0, 0, 0 };

        //antigoAcumulado = (float[])pontosAcumulados.Clone();

        //		int somaDosInteiros;
        for (int i = 1; i < esseNivel; i++)
        {

            antigoAcumulado = (float[])pontosAcumulados.Clone();
            //for(int k=0;k<pontosAcumulados.Length;k++)
            //	antigoAcumulado[k] = pontosAcumulados[k];

            //		Debug.Log(antigoAcumulado);

            for (int j = 0; j < taxas.Length; j++)
            {
                pontosAcumulados[j] = (i + 1) * 5 * taxas[j];

            }

            //			Debug.Log(SomaDosInteiros(pontosAcumulados)%5);
            float[] queRolo = SobraAosEleitos(SomaDosInteiros((float[])pontosAcumulados.Clone()) % 5,
                                              (float[])pontosAcumulados.Clone());

            for (int j = 0; j < taxas.Length; j++)
            {
                //Debug.Log(pontosAcumulados[j]+" : "+(int)pontosAcumulados[j]+" : "+queRolo[j]  );
                pontosAcumulados[j] = (int)pontosAcumulados[j] + queRolo[j];
            }
            /*
                        for(int j=0; j<pontosAcumulados.Length;j++)
                            Debug.Log("Nivel: "+i+
                                      " pontosAcumulados: "+pontosAcumulados[j]+
                                      " diferança de Nivel: "+(pontosAcumulados[j]-antigoAcumulado[j]));

            */

        }

        if (total)
            atualizaAtributos(pontosAcumulados, A, total);
        else
        {
            for (int j = 0; j < pontosAcumulados.Length; j++)
                pontosAcumulados[j] -= antigoAcumulado[j];

            //			Debug.Log(pontosAcumulados[0]+" : cade eu");
            atualizaAtributos(pontosAcumulados, A, total);
        }




        //		Debug.Log(somaDosInteiros);

    }

    static AtributoInstrinseco atualizaAtributoIntrinseco(float pontinhos, AtributoInstrinseco A)
    {
        int valorInicialDeAtributo = 8;
        if (pontinhos > 5)
        {
            
            A = new AtributoInstrinseco(
                valorInicialDeAtributo + (int)pontinhos,
                A.Taxa,
                valorInicialDeAtributo + (int)pontinhos + 5,
                valorInicialDeAtributo + (int)pontinhos - 5,
                A.ModCorrente,
                A.ModMaximo
                );

        }
        else
        {
            
            A = new AtributoInstrinseco(
                valorInicialDeAtributo + (int)pontinhos,
                A.Taxa,
                valorInicialDeAtributo + (int)(2 * pontinhos - 1),
                valorInicialDeAtributo,
                A.ModCorrente,
                A.ModMaximo
                );
        }

        return A;
    }

    static void atualizaAtributos(float[] pontinhos, Atributos A, bool total = false)
    {
        //UnityEngine.Debug.Log(pontinhos[0]+": "+pontinhos[1]);

        if (total)
        {
                A.PV = new AtributoConsumivel(
                    (int)pontinhos[0]*4+10,
                    A.PV.Taxa,
                    4*(int)pontinhos[0]+10,
                    A.PV.ModMaximo
                    );

                A.PE = new AtributoConsumivel(
                    (int)pontinhos[1] * 4 + 26 ,
                    A.PE.Taxa,
                    4 * (int)pontinhos[1] + 26,
                    A.PE.ModMaximo
                    );

            A.Ataque = atualizaAtributoIntrinseco(pontinhos[2], A.Ataque);
            A.Defesa = atualizaAtributoIntrinseco(pontinhos[3], A.Defesa);
            A.Poder  = atualizaAtributoIntrinseco(pontinhos[4], A.Poder);




        }
        else
        {

            A.PV = new AtributoConsumivel(
                A.PV.Corrente +4*(int)pontinhos[0],
                A.PV.Taxa,
                A.PV.Maximo+4*(int)pontinhos[0],
                A.PV.ModMaximo
                );

            A.PE = new AtributoConsumivel(
                A.PE.Corrente + 4*(int)pontinhos[1],
                A.PE.Taxa,
                A.PE.Maximo + 4 * (int)pontinhos[1],
                A.PE.ModMaximo
                );


            A.Ataque = atualizaAtributoIntrinsecoBeta(pontinhos[2], A.Ataque);
            A.Defesa = atualizaAtributoIntrinsecoBeta(pontinhos[3], A.Defesa);
            A.Poder  = atualizaAtributoIntrinsecoBeta(pontinhos[4], A.Poder);

        }
        /*
                for(int  i=0;i<5;i++)
                    Debug.Log(A[i].Basico+" : "+pontinhos[i]+" :"+A[i].Corrente+" : "+A[i].Maximo);
        */
    }

    static AtributoInstrinseco atualizaAtributoIntrinsecoBeta(float pontinhos, AtributoInstrinseco A)
    {
        int j;
        if (pontinhos > 0)
        {
            j = 0;
            while (j<pontinhos)
            {
                if ((A.Minimo + A.Maximo) / 2 + 1 > 5)
                {
                A = new AtributoInstrinseco(
                    A.Corrente+1,
                    A.Taxa,
                    A.Maximo+1,
                    A.Minimo+1,
                    A.ModCorrente,
                    A.ModMaximo
                    );
                               
                }
                else
                {
                    UnityEngine.Debug.Log("<5");
                A = new AtributoInstrinseco(
                    A.Corrente+1,
                    A.Taxa,
                    A.Maximo+2,
                    1,
                    A.ModCorrente,
                    A.ModMaximo
                    );
                   
                }
                j++;
            }
        }

        return A;
    }

    static int SomaDosInteiros(float[] X)
    {
        int Z = 0;
        for (int i = 0; i < X.Length; i++)
            Z += (int)(X[i]);
        return Z;
    }

    static float[] SobraAosEleitos(int distribuidos, float[] pegaRestos)
    {
        int aDistribuir = (distribuidos == 0) ? 0 : 5 - distribuidos;
        if (aDistribuir > 0)
        {
            for (int i = 0; i < pegaRestos.Length; i++)
                pegaRestos[i] -= (int)pegaRestos[i];

            int j;
            float[][] ordenaMaiores = new float[5][];
            float[] temp;
            for (int i = 0; i < pegaRestos.Length; i++)
            {
                ordenaMaiores[i] = new float[2] { i, pegaRestos[i] };
                j = i;
                if (j > 0)
                    while (j > 0 && ordenaMaiores[j][1] >= ordenaMaiores[j - 1][1])
                    {
                        temp = ordenaMaiores[j];
                        ordenaMaiores[j] = ordenaMaiores[j - 1];
                        ordenaMaiores[j - 1] = temp;
                        j--;
                    }
            }

            /*

			for(int i=0;i<pegaRestos.Length;i++)
				Debug.Log(ordenaMaiores[i][0]);

*/
            pegaRestos = new float[5] { 0, 0, 0, 0, 0 };

            for (int i = 0; i < aDistribuir; i++)
            {
                //				Debug.Log(ordenaMaiores[i][0]+" : "+aDistribuir);
                pegaRestos[(int)ordenaMaiores[i][0]] = 1;
            }

        }
        else
            pegaRestos = new float[5] { 0, 0, 0, 0, 0 };


        return pegaRestos;
    }
}
