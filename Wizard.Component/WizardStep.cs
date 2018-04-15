namespace Wizard.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
        
    public class WizardStep
    {        
        private Dictionary<string, object> bindings;
                
        public WizardStep(string id, string title, bool isCompleted = false)
        {
            this.Id = id;
            this.Title = title;
            this.IsCompleted = isCompleted;
            this.bindings = new Dictionary<string, object>();
        }
                
        public string Id
        {
            get;
            private set;
        }
                
        public string Title
        {
            get;
            private set;
        }
                
        public bool IsCompleted
        {
            get;
            set;
        }
                
        public object this[string key]
        {
            get
            {
                object value;

                if (this.bindings.TryGetValue(key, out value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }

            set
            {
                if (this.bindings == null)
                {
                    this.bindings = new Dictionary<string, object>();
                }

                this.bindings[key] = value;
            }
        }
    }
}
