using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Noeud 
{

	private int m_g; //la distance parcourue depuis le point de départ pour arriver au node courant.
	private int m_h; //la distance à vol d'oiseau entre le node courant et le node d'arrivée.
	private int m_f; // somme de G et de H -> poid

	//info cellule
	private int m_col; // col du graphe
	private int m_line; // ligne du graph

	protected bool m_walkable;  // obstacle?
	private Noeud m_parent;  // le parent de ce Node en cours



	public  Noeud()
	{
		m_walkable = false;
		m_g = m_h = m_f = 0;
		m_parent = this;
	}

	public Noeud(bool walkable, int x, int y)
	{
		m_walkable = walkable;
		m_col = x;
		m_line = y;

		m_g = m_h = m_f = 0;
		m_parent = this;
	}


	public int g {get { return m_g; } set { m_g = value; }}
	public int h {get { return m_h; } set { m_h = value; }}
	public Noeud parent {get { return m_parent; } set { m_parent = value; }}

	
	public void ActuPoid(){ m_f=m_g+m_h;}
	public int poid{ get {ActuPoid(); return m_f;}}

	public void SetCellules(int x, int y){m_col=x; m_line =y;}

	public Vector2 ceil { get {return new Vector2(m_col,m_line);}}
	public int x { get {return m_col;} set {m_col=value;}}
	public int y { get {return m_line;} set {m_line=value;}}

    public bool EstOccupe { get { return m_walkable; } set { m_walkable = value; } }


}
