const prismSheet = new CSSStyleSheet();

const prismSheetReady = fetch('https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css')
    .then(response => response.text())
    .then(css => {
        prismSheet.replaceSync(css);
    });

class ProjectStep extends HTMLElement
{
    constructor()
    {
        super();

        const shadow = this.attachShadow({mode: "open"});

        shadow.innerHTML = `
            <link rel="stylesheet" href="../projects.css">
            <div class="step"><input type="checkbox" id="step"><label for="step"><p><slot></slot></p></label></div>
        `;
    }

    connectedCallback() 
    {
        if (this.parentElement?.closest('project-step')) 
        {
            this.classList.add('nested');
        }
    }

    static get observedAttributes()
    {
        return ['step-figure', 'step-figure-caption'];
    }

    attributeChangedCallback(name, oldValue, newValue)
    {
        if(name === 'step-figure') 
        {
            let element = this.shadowRoot.querySelector('div');
            element.innerHTML += `
                <figure><img src = "` + newValue + `"></figure>
            `;
        }
        else if(name === 'step-figure-caption')
        {
            let element = this.shadowRoot.querySelector('figure');
            element.innerHTML += `
                <figcaption>` + newValue + `</figcaption>
            `;
        }
    }

    get stepFigure()
    {
        return this.getAttribute('step-figure');
    }

    get stepFigureCaption()
    {
        return this.getAttribute('step-figure-caption');
    }

    set stepFigure(val)
    {
        this.setAttribute('step-figure', val);
    }

    set stepFigureCaption(val)
    {
        this.setAttribute('step-figure-caption', val);
    }
}

class ProjectCode extends HTMLElement
{
    constructor()
    {
        super();

        const shadow = this.attachShadow({mode: "open"});

        shadow.adoptedStyleSheets = [prismSheet];

        shadow.innerHTML = `
            <pre style="background-color: whitesmoke;"><code class="language-gdscript" style="font-size: smaller;"></code></pre>
            <slot hidden></slot>
        `;

        const slot = shadow.querySelector('slot');

        slot.addEventListener('slotchange', () => {
            const source = slot.assignedNodes()
                .map(node => node.textContent)
                .join('');

            const code = shadow.querySelector('code');

            code.textContent = source;

            Prism.highlightElement(code);
        });
    }

    connectedCallback()
    {
        
    }
}

customElements.define('project-step', ProjectStep);
customElements.define('project-code', ProjectCode);