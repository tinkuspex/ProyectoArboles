using ArbolService.Models;
using System.Web.Http.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ArbolService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        // GET api/values
        ConexionBD dalConnection = new ConexionBD();
        DaJerarquia daJerarquia;
        List<Dictionary<string, Dataframe>> parents;

        [Route("api/RelacionMalla")]
        [HttpGet]
        public IHttpActionResult RelacionMalla([FromUri] string malla, [FromUri] string job)
        {
            try
            {
                List<RelacionMallas> lista = new List<RelacionMallas>();
                daJerarquia = new DaJerarquia(dalConnection);
                lista = daJerarquia.ObtenerListaRelacionMallas(malla);
                RelacionMallas rl = new RelacionMallas();
                rl.Parent = job;
                rl.Child = "";
                lista.Add(rl);
                //int longitud_lista = lista.Count();
                List<Dataframe> df = new List<Dataframe>();
                foreach (var item in lista)
                {
                    Dataframe df1 = new Dataframe();
                    df1.eid = item.Parent;
                    df1.Parent = item.Parent;
                    df1.Child = item.Child;
                    df.Add(df1);
                }
                parents = new List<Dictionary<string, Dataframe>>();
                foreach (var item in df)
                {
                    Dictionary<string, Dataframe> parent = new Dictionary<string, Dataframe>();
                    parent[item.Child] = item;
                    parents.Add(parent);
                }
                var tree = buildtree();
                //var json = Newtonsoft.Json.JsonConvert.SerializeObject(lista);
                return Ok(tree);
            }
            catch (Exception ex)
            {

                return Ok(ex);
            }
        }

        public dynamic buildtree(dynamic t = null, string parent_eid = "")
        {
            Dictionary<string, dynamic> report;
            List<Dictionary<string, Dataframe>> parent = parents.FindAll(m => m.ContainsKey(parent_eid));
            if (parent == null)
            {
                return t;
            }
            foreach (var item in parent)
            {
                if (item[parent_eid].Child == "")
                {
                    report = new Dictionary<string, dynamic>()    {
                        {"parent", "null"},
                        {"name", item[parent_eid].Parent},
                        {"edge_name", item[parent_eid].Parent}
                    };
                }
                else
                {
                    report = new Dictionary<string, dynamic>()    {
                       {"parent", item[parent_eid].Child},
                       {"name",  item[parent_eid].Parent},
                       {"edge_name",  item[parent_eid].Parent}

                    };
                }
                if (t == null)
                {
                    t = report;
                }
                else
                {
                    List<Dictionary<string, dynamic>> dic = new List<Dictionary<string, dynamic>>();

                    metodos.SetDefault(t, "children", dic);
                    dynamic reports = t;
                    reports["children"].Add(report);

                }
                buildtree(report, item[parent_eid].eid);
            }
            return t;
        }
        [Route("api/Malla")]
        [HttpGet]
        public IHttpActionResult Malla()
        {
            try
            {
                List<Malla> lista = new List<Malla>();
                daJerarquia = new DaJerarquia(dalConnection);
                lista = daJerarquia.ObtenerListaMallas();

                return Ok(lista);
            }
            catch (Exception ex)
            {

                return Ok(ex);
            }
        }
        [Route("api/Jobs")]
        [HttpGet]
        public IHttpActionResult Jobns([FromUri] string malla)
        {
            try
            {
                List<Jobs> lista = new List<Jobs>();
                daJerarquia = new DaJerarquia(dalConnection);
                lista = daJerarquia.ObtenerListaJobs(malla);

                return Ok(lista);
            }
            catch (Exception ex)
            {

                return Ok(ex);
            }
        }

        // Añadiendo Código
        [Route("api/Predecesores")]
        [HttpGet]
        public IHttpActionResult Predecesores([FromUri] string malla, [FromUri] string job)
        {
            try
            {
                List<RelacionMallas> lista = new List<RelacionMallas>();
                daJerarquia = new DaJerarquia(dalConnection);
                lista = daJerarquia.ObtenerListaPredecesores(malla);
                RelacionMallas rl = new RelacionMallas();
                rl.Parent = job;
                rl.Child = "";
                lista.Add(rl);
                //int longitud_lista = lista.Count();
                List<Dataframe> df = new List<Dataframe>();
                foreach (var item in lista)
                {
                    Dataframe df1 = new Dataframe();
                    df1.eid = item.Parent;
                    df1.Parent = item.Parent;
                    df1.Child = item.Child;
                    df.Add(df1);
                }
                parents = new List<Dictionary<string, Dataframe>>();
                foreach (var item in df)
                {
                    Dictionary<string, Dataframe> parent = new Dictionary<string, Dataframe>();
                    parent[item.Child] = item;
                    parents.Add(parent);
                }
                var tree = buildtree();
                //var json = Newtonsoft.Json.JsonConvert.SerializeObject(lista);
                return Ok(tree);
            }
            catch (Exception ex)
            {

                return Ok(ex);
            }
        }

    }
}
