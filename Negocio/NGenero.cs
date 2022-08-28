using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

static class NGenero{
  private static List<Genero> generos;
  public static void Inserir(Genero g){
    generos = Abrir();
    int id = 0;
    foreach(Genero i in generos)
      if(i.Id > id) id = i.Id;
    id++;
    g.Id = id;
    generos.Add(g);
    Salvar(generos);
  }
  public static List<Genero> Listar(){
    generos = Abrir();
    return generos.OrderBy(obj => obj.Id).ToList();
  }
  public static void Atualizar(Genero g){
    Genero x = Pesquisar(g.Id);
    if(x==null) throw new NullReferenceException("Não existe Gêneros");
    x.Descricao = g.Descricao;
      
  }
  public static void Excluir(Genero g){
    Genero x = Pesquisar(g.Id);
    if(x != null){
      generos.Remove(x);
      Salvar(generos);
    }
    else throw new ArgumentOutOfRangeException("Não existe Gêneros");
  }
  public static Genero Pesquisar(int id){
    generos = Abrir();
    foreach(Genero i in generos)
      if(i.Id == id) return i;
    return null;
  }
  
  private static string arquivo = "./generos.xml";
  
  private static List<Genero> Abrir() {
    XmlSerializer xml = new XmlSerializer(typeof(List<Genero>));
    StreamReader f = null;
    List<Genero> obj;
    try {
      f = new StreamReader(arquivo);
      obj = (List<Genero>) xml.Deserialize(f);
    }
    catch(FileNotFoundException) {
      obj = new List<Genero>();
    }
    finally {
     if (f != null) f.Close();
    }
    return obj;
  }
  private static void Salvar(List<Genero> obj) {
    XmlSerializer xml = new
      XmlSerializer(typeof(List<Genero>));
    StreamWriter f = new StreamWriter(arquivo, false);
    xml.Serialize(f, obj);
    f.Close();
  }
}