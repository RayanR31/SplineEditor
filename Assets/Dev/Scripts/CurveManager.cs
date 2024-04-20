using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CurveManager : MonoBehaviour
{
    /// <summary>
    /// Ce script sert � cr�er une courbe de b�zier, utilisable directement depuis l'inspector. 
    /// Il permet d'ajouter des gameObject sur la courbe utile pour placer des objets qui se r�p�te comme des pattouns.
    /// Plus tard des options seront rajout� pour une IA (State-Machine)
    /// 
    /// La premi�re partie du code sert � visualiser la courbe ainsi que la manipuler, et de l'initaliser correctement.
    /// La seconde partie du code sert � ce que l'utilisateur puisse la manipuler depuis l'inspector.
    /// </summary>
    /// 

    // Une liste de points qui va permettre de manipuler la courbe
    [Tooltip("Liste de point de la courbe de B�zier")][SerializeField] private List<GameObject> points;

    // Le gameObject � faire spawn
    [Tooltip("GameObject � faire spawn")][SerializeField] private GameObject prefab;

    // Valeur pas indispensalbe, mais la laisse accessibles pour les curieux. Il s'agit du visuel de la courbe
    [Tooltip("Visuelle de la courbe")][Range(0.001f, 0.1f)][SerializeField] private float stepVisual = 0.001f;

    // Le nombre de gameObject � faire spawn sur la courbe
    [Tooltip("Nombre de gameObject � faire spawn")][Range(0, 200)] [SerializeField] private int numberGameObject;

    // Pour visualiser la courbe ou non sur l'�cran
    [Tooltip("Voir la courbe ou non")][SerializeField] private bool viewCurve;

    // Index qui va servir � instancier les gameObject �quitablement sur chaque courbe
    private int index ;

    // Dossier o� l'on range les points de la courbe de b�zier
    private GameObject folderPoints;

    // Dossier o� l'on range les gameObjects
    private GameObject folderGameObject; 

    /// <summary>
    /// 
    /// 1ER PARTIE : Cr�ation de la courbe et visualisation de la courbe
    /// 
    /// </summary>
    void OnDrawGizmos()
    {
        // Cette m�thode va servir � initialiser correctement la courbe
        Init();
        // Cette m�thode va servir � dessiner la courbe
        DrawCurve();
    }


    /// <summary>
    /// Cette m�thode sert � initialiser correctement les premiers points ABC
    /// </summary>
    void Init()
    {
        // Ajoute un dossier pour les points au cas o� si il est n'est pas pr�sent
        if (folderPoints == null)
        {
            folderPoints = new GameObject("FolderPoints");
            folderPoints.transform.SetParent(transform);
        }

        // Ajoute un dossier pour les gameObject au cas o� si il est n'est pas pr�sent
        if (folderGameObject == null)
        {
            folderGameObject = new GameObject("FolderGameObject");
            folderGameObject.transform.SetParent(transform);
        }

        // Si la list est vide ajoute 3 �l�ments dans la list ABC 
        if (points.Count == 0)
        {
            AddPointManually("PosInitial", new Vector3(0, 0, 0), true, 0);
            AddPointManually("Tangente 1", new Vector3(1, 1.5f, 0), false, 1);
            AddPointManually("Destination 1", new Vector3(2, 0, 0), true, 2);
        }

        // S�curit� au cas ou si l'utilisateur supprimer les premiers points � la main
        if (points[2] == null)
        {
            SecurityPoint("PosInitial", new Vector3(0, 0, 0), true, 0);
            SecurityPoint("Tangente 1", new Vector3(1, 1.5f, 0), false, 1);
            SecurityPoint("Destination 1", new Vector3(2, 0, 0), true, 2);
        }
    }



    /// <summary>
    /// Cette m�thode va servir � dessiner la courbe.
    /// Cette courbe sera visible uniquement si l'utilisateur passe en TRUE le param�tre ViewCurve
    /// 
    /// La premi�re boucle sert � faire les calculs de la courbe de b�zier
    /// 
    /// Imaginer qu'on a des points ABC, 
    /// A = i = Position initial
    /// B = i + 1 = Tangente
    /// C = i + 2 = Destination
    /// 
    /// Pourquoi i += 2, pour �viter de tracer aussi les courbes de la tangente. 
    /// Car si on ne fait pas �a, lors de l'ajout d'une courbe cela ferait 
    /// ABC - BCB - ABC au lieu de ABC - ABC - ABC
    /// La deuxi�me boucle for sert � dessiner la courbe correctement avec un ratio t
    /// </summary>
    void DrawCurve()
    {
        if (viewCurve)
        {
            for (int i = 0; i < points.Count - 2; i += 2)
            {
                for (float t = 0; t <= 1; t += stepVisual)
                {
                    Vector3 _AB = Vector3.Lerp(points[i].transform.position, points[i + 1].transform.position, t);
                    Vector3 _BC = Vector3.Lerp(points[i + 1].transform.position, points[i + 2].transform.position, t);
                    Vector3 posFinal = Vector3.Lerp(_AB, _BC, t);
                    Vector3 posLineMax = Vector3.Lerp(_AB, _BC, t + stepVisual);

                    Gizmos.DrawLine(posFinal, posLineMax);
                }
            }
        }
    }

    /// <summary>
    /// Cette m�thode sert � ajouter des points manuellements dans le script au cas o� si la list est vide
    /// </summary>
    /// <param name="_name">Le nom du gameObject qui va �tre cr�e</param>
    /// <param name="pos">La position initial que va prendre le gameObject</param>
    /// <param name="CustomPoint">Si il s'agit d'un point initial ou de destination alors il prends le visual point sinon il prends le visual tangente</param>
    /// <param name="index">Ou ranger l'objet dans la list</param>
    void AddPointManually(string _name , Vector3 pos , bool CustomPoint , int index)
    {
        GameObject point = new GameObject(_name);
        point.transform.position = pos;
        point.transform.SetParent(folderPoints.transform);

        if(CustomPoint) point.AddComponent<CustomVisualPoint>();
        else point.AddComponent<CustomVisualTangente>();

        points.Insert(index, point);
    }
    /// <summary>
    /// Cette m�thode de s�curit� sert � ajouter des points manuellements dans le script au cas o� si la list est pleine mais null
    /// </summary>
    /// <param name="_name">Le nom du gameObject qui va �tre cr�e</param>
    /// <param name="pos">La position initial que va prendre le gameObject</param>
    /// <param name="CustomPoint">Si il s'agit d'un point initial ou de destination alors il prends le visual point sinon il prends le visual tangente</param>
    /// <param name="index">Ou ranger l'objet dans la list</param>
    void SecurityPoint(string _name, Vector3 pos, bool CustomPoint, int index)
    {
        GameObject point = new GameObject(_name);
        point.transform.position = pos;
        point.transform.SetParent(folderPoints.transform);

        if (CustomPoint) point.AddComponent<CustomVisualPoint>();
        else point.AddComponent<CustomVisualTangente>(); 

        points[index] = point;
    }
    /// <summary>
    /// 
    /// 2EME PARTIE : Interaction de l'utilisateur avec la courbe gr�ce � des boutons
    /// 
    /// </summary>
    /// 

    /// <summary>
    /// 
    /// Cette m�thode sert � vider et g�n�rer de nouveau gameObject
    /// 
    /// </summary>
    /// 
    public void GenerateGameObject()
    {
        Clear();
        InstantiateGameObject();
    }

    /// <summary>
    /// 
    /// Cette m�thode sert � ajouter des gameObject tout en conservant les anciens
    /// 
    /// </summary>
    /// 
    public void AddGameObject()
    {
        InstantiateGameObject();
    }

    /// <summary>
    /// Cette m�thode va servir � spawn �quitablement les gameObject sur la courbe.
    /// 
    /// La premi�re boucle sert � faire les calculs de la courbe de b�zier
    /// 
    /// Imaginer qu'on a des points ABC, 
    /// A = i = Position initial
    /// B = i + 1 = Tangente
    /// C = i + 2 = Destination
    /// 
    /// Pourquoi i += 2, pour �viter de spawn les gameObject aussi sur les courbes de la tangente. 
    /// Car si on ne fait pas �a, lors de l'ajout d'une courbe cela ferait 
    /// ABC - BCB - ABC au lieu de ABC - ABC - ABC
    /// La deuxi�me boucle for sert � positionner les gameObject �quitablement sur la courbe
    /// </summary>
    void InstantiateGameObject()
    {
        for (int i = 0; i < points.Count - 2; i += 2)
        {
            for (int j = index; j < numberGameObject; j++)
            {
                Vector3 _AB = Vector3.Lerp(points[i].transform.position, points[i + 1].transform.position, j / (float)numberGameObject);
                Vector3 _BC = Vector3.Lerp(points[i + 1].transform.position, points[i + 2].transform.position, j / (float)numberGameObject);
                Vector3 posLine = Vector3.Lerp(_AB, _BC, j / (float)numberGameObject);
                Instantiate(prefab, posLine, Quaternion.identity, folderGameObject.transform);
                index++;
            }

            index = 0;
        }
    }

    /// <summary>
    /// Sert � nettoyer les gameObject de la courbe 
    /// </summary>
    public void Clear()
    {
        for (int i = folderGameObject.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(folderGameObject.transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// Cette m�thode sert � recommencer la courbe � 0 cela conserve les gameObject
    /// </summary>
    public void ResetCurve()
    {
        for (int i = folderPoints.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(folderPoints.transform.GetChild(i).gameObject);
        }
        points.Clear();
    }

    /// <summary>
    /// Cette m�thode sert � ajouter des points B (Tangente) et C (Destination) sur la courbe
    /// </summary>
    public void AddPoint()
    {

        GameObject pointB = new GameObject("Tangente " + (points.Count - 1));
        pointB.transform.position = new Vector3(points[points.Count - 1].transform.position.x + 2f, 1.5f, 0);
        points.Insert(points.Count, pointB);
        pointB.transform.SetParent(folderPoints.transform);
        pointB.AddComponent<CustomVisualTangente>();

        GameObject pointC = new GameObject("Destination " + (points.Count - 2));
        pointC.transform.position = new Vector3(points[points.Count - 1].transform.position.x + 2.5f, 0, 0);
        pointC.transform.SetParent(folderPoints.transform);
        points.Insert(points.Count, pointC);
        pointC.AddComponent<CustomVisualPoint>();

    }

    /// <summary>
    /// Sert � supprimer les deux derniers points ajouter dans la liste
    /// </summary>
    public void DeletePoint()
    {
        if(points.Count > 3)
        {
            DestroyImmediate(points[points.Count - 2].gameObject);
            DestroyImmediate(points[points.Count - 1].gameObject);
            points.RemoveRange(points.Count - 2, 2);
        }
    }

}
