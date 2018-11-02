using UnityEngine;
using System.Collections;

public interface IGolpeBase
{
    nomesGolpes Nome { get; }
    nomeTipos Tipo { get; }
    caracGolpe Caracteristica { get; }
    bool PodeNoAr { get;}

    int PotenciaCorrente { get; }
    int DanoMaximo { get; }
    int ModCorrente { get; set; }
    int CustoPE { get; }

    float TempoDeReuso { get; }
    float ModTempoDeReuso { get; set; }
    float UltimoUso { get; set; }
    float DistanciaDeRepulsao { get; }
    float VelocidadeDeRepulsao { get; }
    float TempoNoDano { get; }

    float VelocidadeDeGolpe { get; }
    float TempoDeMoveMin { get; }
    float TempoDeMoveMax { get; }
    float TempoDeDestroy { get; }

    float ColisorScale { get; }

    Vector3 DirDeREpulsao { get; set; }

    void IniciaGolpe(GameObject G);
    void UpdateGolpe(GameObject G);
    void FinalizaEspecificoDoGolpe();
    void VerificaAplicaStatus(CriatureBase atacante, CreatureManager atacado);

}

[System.Serializable]
public class ContainerDeCaracteristicasDeGolpe
{
    public nomesGolpes nome = nomesGolpes.nulo;
    public nomeTipos tipo = nomeTipos.Normal;
    public caracGolpe carac = caracGolpe.colisao;
    public bool podeNoAr = false;

    public int potenciaMinima = 1;
    public int potenciaCorrente = 2;
    public int potenciaMaxima = 3;
    public int danoMaximo = 8;
    public int modCorrente = 0;
    public int custoPE = 0;

    public float tempoDeReuso = 3.5f;
    public float modTempoDeReuso = 0;
    public float ultimoUso = -100;
    public float distanciaDeRepulsao = 55;
    public float velocidadeDeRepulsao = 27;
    public float TempoNoDano = 0.25f;

    public float velocidadeDeGolpe = 18f;
    public float tempoDeMoveMin = 0.25f;
    public float tempoDeMoveMax = 0.5f;
    public float tempoDeDestroy = 1;

    public float colisorScale = 1;
}



public enum caracGolpe
{
    projetil,
    colisao,
    colisaoComPow,
    area,
    suporte,
    projetilPerseguidor,
    especialComParalisia,
    hitNoChao

}