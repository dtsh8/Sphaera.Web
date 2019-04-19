import React from 'react';
import { Grid, Col, Row } from 'react-bootstrap';
import ReactQuill from 'react-quill';
import 'react-quill/dist/quill.snow.css';
import katex from 'katex';
import 'katex/dist/katex.min.css';
import bem from '../../../lib/bem';
import { RemindersMenu } from '../RemindersMenu';
import './style.css';

const block = 'reminders-content';

window.katex = katex;

export class RemindersContent extends React.PureComponent {
  constructor(props, context) {
    super(props, context);

    this.quillRef = null;
    this.reactQuillRef = null;

    this.attachQuillRefs = this.attachQuillRefs.bind(this);
  }

  componentDidMount() {
    this.attachQuillRefs();
  }

  componentDidUpdate() {
    this.attachQuillRefs();

    this.insertData();
  }

  attachQuillRefs() {
    if (typeof this.reactQuillRef.getEditor !== 'function') return;
    if (this.quillRef != null) return;

    const quillRef = this.reactQuillRef.getEditor();
    if (quillRef != null) {
      this.quillRef = quillRef;
    }
  }

  insertData = () => {
    const { data } = this.props;

    if (JSON.stringify(this.quillRef.getContents()) !== data.Content) {
      this.quillRef.setContents([], 'api');
      if (data.Content) {
        this.quillRef.setContents(JSON.parse(data.Content).ops, 'api');
      }
    }
  };

  render() {
    const { 
      data,
      list,
      changeReminderPage,
      expandedNodes,
      setReminderNodeExpanded,
      setReminderNodeCollapsed
    } = this.props;

    return (
      <Grid>
        <div className={bem({ block })}>
          <Row>
            <Col xs={12}>
              <Row>
                <Col lg={8} md={8} sm={12}>
                  <div>
                    <h3 className={bem({ block, elem: 'title-content' })}>
                      {data.Name}
                    </h3>
                    <span className={bem({ block, elem: 'content' })}>
                      <ReactQuill
                        theme="snow"
                        readOnly
                        ref={(el) => { this.reactQuillRef = el; }}
                        modules={
                          {
                            toolbar: false
                          }
                        }
                      />
                    </span>
                  </div>
                </Col>

                <div className={bem({ block, elem: 'other-reminders' })}>
                  Другие памятки и инструкции
                </div>

                <Col lg={4} md={4} sm={5}>
                  <RemindersMenu
                    data={list}
                    changeActiveReminder={changeReminderPage}
                    activeAccordionItemId={data.Id}
                    expandedNodes={expandedNodes}
                    setReminderNodeExpanded={setReminderNodeExpanded}
                    setReminderNodeCollapsed={setReminderNodeCollapsed}
                  />
                </Col>
              </Row>
            </Col>
          </Row>
        </div>
      </Grid>
    );
  }
}
