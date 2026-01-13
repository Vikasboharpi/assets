// Custom Swagger UI JavaScript
window.addEventListener('DOMContentLoaded', function() {
    // Add custom functionality after Swagger UI loads
    setTimeout(function() {
        addCustomFeatures();
    }, 1000);
});

function addCustomFeatures() {
    // Add JWT token helper
    addJWTTokenHelper();
    
    // Add custom header
    addCustomHeader();
    
    // Add keyboard shortcuts
    addKeyboardShortcuts();
    
    // Add response time display
    addResponseTimeDisplay();
}

function addJWTTokenHelper() {
    // Create a helper section for JWT token
    const infoSection = document.querySelector('.swagger-ui .info');
    if (infoSection && !document.querySelector('.jwt-helper')) {
        const jwtHelper = document.createElement('div');
        jwtHelper.className = 'jwt-helper';
        jwtHelper.innerHTML = `
            <div style="background: #e8f5e8; border: 1px solid #27ae60; border-radius: 4px; padding: 15px; margin: 20px 0;">
                <h4 style="color: #27ae60; margin-bottom: 10px;">🔐 JWT Authentication Helper</h4>
                <p style="margin-bottom: 10px; color: #2c3e50;">
                    To test protected endpoints:
                </p>
                <ol style="color: #34495e; margin-left: 20px;">
                    <li>First, call <strong>POST /api/auth/login</strong> with valid credentials</li>
                    <li>Copy the token from the response</li>
                    <li>Click the <strong>"Authorize"</strong> button above</li>
                    <li>Enter: <code>Bearer &lt;your-token&gt;</code></li>
                    <li>Click <strong>"Authorize"</strong> and then <strong>"Close"</strong></li>
                </ol>
                <div style="margin-top: 15px; padding: 10px; background: #fff3cd; border: 1px solid #ffeaa7; border-radius: 4px;">
                    <strong>💡 Tip:</strong> Default admin credentials - Email: <code>admin@assetmanagement.com</code>, Password: <code>Admin@123</code>
                </div>
            </div>
        `;
        infoSection.appendChild(jwtHelper);
    }
}

function addCustomHeader() {
    // Add custom header with API information
    const topbar = document.querySelector('.swagger-ui .topbar');
    if (topbar && !document.querySelector('.custom-header')) {
        const customHeader = document.createElement('div');
        customHeader.className = 'custom-header';
        customHeader.innerHTML = `
            <div style="color: white; padding: 10px 20px; font-size: 14px;">
                <span style="font-weight: bold;">Asset Management API</span>
                <span style="margin-left: 20px; opacity: 0.8;">Environment: Development</span>
                <span style="margin-left: 20px; opacity: 0.8;">Version: 1.0.0</span>
            </div>
        `;
        topbar.appendChild(customHeader);
    }
}

function addKeyboardShortcuts() {
    // Add keyboard shortcuts
    document.addEventListener('keydown', function(e) {
        // Ctrl/Cmd + K to focus on authorize button
        if ((e.ctrlKey || e.metaKey) && e.key === 'k') {
            e.preventDefault();
            const authorizeBtn = document.querySelector('.swagger-ui .btn.authorize');
            if (authorizeBtn) {
                authorizeBtn.click();
            }
        }
        
        // Escape to close modals
        if (e.key === 'Escape') {
            const closeBtn = document.querySelector('.swagger-ui .modal .close-modal');
            if (closeBtn) {
                closeBtn.click();
            }
        }
    });
}

function addResponseTimeDisplay() {
    // Monitor API calls and display response times
    const originalFetch = window.fetch;
    window.fetch = function(...args) {
        const startTime = performance.now();
        return originalFetch.apply(this, args).then(response => {
            const endTime = performance.now();
            const responseTime = Math.round(endTime - startTime);
            
            // Add response time to the response display
            setTimeout(() => {
                const responseSection = document.querySelector('.swagger-ui .responses-wrapper .live-responses-table');
                if (responseSection && !responseSection.querySelector('.response-time')) {
                    const responseTimeElement = document.createElement('div');
                    responseTimeElement.className = 'response-time';
                    responseTimeElement.innerHTML = `
                        <div style="background: #f8f9fa; border: 1px solid #dee2e6; border-radius: 4px; padding: 8px; margin: 10px 0; font-size: 12px; color: #6c757d;">
                            ⏱️ Response Time: <strong>${responseTime}ms</strong>
                        </div>
                    `;
                    responseSection.insertBefore(responseTimeElement, responseSection.firstChild);
                }
            }, 100);
            
            return response;
        });
    };
}

// Add copy to clipboard functionality for code blocks
function addCopyToClipboard() {
    document.addEventListener('click', function(e) {
        if (e.target.classList.contains('copy-btn')) {
            const codeBlock = e.target.nextElementSibling;
            if (codeBlock) {
                navigator.clipboard.writeText(codeBlock.textContent).then(() => {
                    e.target.textContent = 'Copied!';
                    setTimeout(() => {
                        e.target.textContent = 'Copy';
                    }, 2000);
                });
            }
        }
    });
}

// Initialize copy to clipboard
addCopyToClipboard();

// Add custom styles for better mobile experience
function addMobileStyles() {
    const style = document.createElement('style');
    style.textContent = `
        @media (max-width: 768px) {
            .swagger-ui .info .title {
                font-size: 1.8em !important;
            }
            
            .swagger-ui .opblock-summary {
                flex-wrap: wrap;
            }
            
            .swagger-ui .opblock-summary-method {
                margin-bottom: 5px;
            }
            
            .jwt-helper ol {
                font-size: 14px;
            }
            
            .custom-header {
                font-size: 12px !important;
            }
            
            .custom-header span {
                display: block !important;
                margin-left: 0 !important;
                margin-top: 2px;
            }
        }
    `;
    document.head.appendChild(style);
}

// Initialize mobile styles
addMobileStyles();

// Add loading indicator for API calls
function addLoadingIndicator() {
    const style = document.createElement('style');
    style.textContent = `
        .swagger-ui .btn.execute.loading {
            position: relative;
            color: transparent !important;
        }
        
        .swagger-ui .btn.execute.loading::after {
            content: '';
            position: absolute;
            width: 16px;
            height: 16px;
            top: 50%;
            left: 50%;
            margin-left: -8px;
            margin-top: -8px;
            border: 2px solid #ffffff;
            border-radius: 50%;
            border-top-color: transparent;
            animation: spin 1s linear infinite;
        }
        
        @keyframes spin {
            to {
                transform: rotate(360deg);
            }
        }
    `;
    document.head.appendChild(style);
    
    // Add loading state to execute buttons
    document.addEventListener('click', function(e) {
        if (e.target.classList.contains('execute')) {
            e.target.classList.add('loading');
            setTimeout(() => {
                e.target.classList.remove('loading');
            }, 2000);
        }
    });
}

// Initialize loading indicator
addLoadingIndicator();

console.log('🚀 Asset Management API - Custom Swagger UI features loaded successfully!');